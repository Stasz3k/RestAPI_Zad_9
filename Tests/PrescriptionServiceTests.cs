using RestAPI_Zad_9.Models.DTOs;
using RestAPI_Zad_9.Models;
using RestAPI_Zad_9.Repos;
using Xunit;
using RestAPI_Zad_9.Services;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestAPI_Zad_9.Tests
{
    public class PrescriptionServiceTests
    {
        private readonly Mock<IPrescriptionRepository> _repositoryMock;
        private readonly IPrescriptionService _service;

        public PrescriptionServiceTests()
        {
            _repositoryMock = new Mock<IPrescriptionRepository>();
            _service = new PrescriptionService(_repositoryMock.Object);
        }

        [Fact]
        public async Task AddPrescriptionAsync_ShouldAddPrescription_WhenValidData()
        {

            var prescriptionDto = new PrescriptionDto
            {
                Date = DateTime.Now,
                DueDate = DateTime.Now.AddDays(10),
                Patient = new PatientDto
                {
                    IdPatient = 1,
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    BirthDate = new DateTime(1980, 1, 1)
                },
                Doctor = new DoctorDto
                {
                    IdDoctor = 1,
                    FirstName = "Anna",
                    LastName = "Nowak",
                    Email = "annanowak@o2.pl"
                },
                Medicaments = new List<MedicamentDto>
            {
                new MedicamentDto
                {
                    IdMedicament = 1,
                    Name = "Medicament1",
                    Dose = 1,
                    Details = "Details1"
                }
            }
            };

            _repositoryMock.Setup(r => r.GetMedicamentByIdAsync(It.IsAny<int>())).ReturnsAsync(new Medicament());
            _repositoryMock.Setup(r => r.AddPrescriptionAsync(It.IsAny<Prescription>())).Returns(Task.CompletedTask);

            await _service.AddPrescriptionAsync(prescriptionDto);

            _repositoryMock.Verify(r => r.AddPrescriptionAsync(It.IsAny<Prescription>()), Times.Once);
        }

        [Fact]
        public async Task GetPatientWithPrescriptionsAsync_ShouldReturnPatient_WhenPatientExists()
        {
            var patientId = 1;
            var patient = new Patient
            {
                IdPatient = patientId,
                FirstName = "Jan",
                LastName = "Kowalski",
                Birthdate = new DateTime(1980, 1, 1),
                Prescriptions = new List<Prescription>
            {
                new Prescription
                {
                    IdPrescription = 1,
                    Date = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(10),
                    Doctor = new Doctor
                    {
                        IdDoctor = 1,
                        FirstName = "Anna",
                        LastName = "Nowak",
                        Email = "annanowak@o2.pl"
                    },
                    PrescriptionMedicaments = new List<PrescriptionMedicament>
                    {
                        new PrescriptionMedicament
                        {
                            IdMedicament = 1,
                            Medicament = new Medicament
                            {
                                IdMedicament = 1,
                                Name = "Medicament1"
                            },
                            Dose = 1,
                            Details = "Details1"
                        }
                    }
                }
            }
            };

            _repositoryMock.Setup(r => r.GetPatientWithPrescriptionsAsync(patientId)).ReturnsAsync(patient);

            var result = await _service.GetPatientWithPrescriptionsAsync(patientId);

            Assert.IsNotNull(result);
            Assert.AreEqual(patientId, result.IdPatient);
        }
    }

}
