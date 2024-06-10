using RestAPI_Zad_9.Models.DTOs;
using RestAPI_Zad_9.Models;
using RestAPI_Zad_9.Services;
using RestAPI_Zad_9.Repos;

public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _repository;

    public PrescriptionService(IPrescriptionRepository repository)
    {
        _repository = repository;
    }

    public async Task AddPrescriptionAsync(PrescriptionDto prescriptionDto)
    {
        var patient = await _repository.GetPatientWithPrescriptionsAsync(prescriptionDto.Patient.IdPatient)
            ?? new Patient
            {
                FirstName = prescriptionDto.Patient.FirstName,
                LastName = prescriptionDto.Patient.LastName,
                Birthdate = prescriptionDto.Patient.BirthDate
            };

        if (prescriptionDto.Medicaments.Count > 10)
        {
            throw new Exception("Prescription cannot contain more than 10 medicaments.");
        }

        foreach (var medicamentDto in prescriptionDto.Medicaments)
        {
            var medicament = await _repository.GetMedicamentByIdAsync(medicamentDto.IdMedicament);
            if (medicament == null)
            {
                throw new Exception($"Medicament with Id {medicamentDto.IdMedicament} does not exist.");
            }
        }

        if (prescriptionDto.DueDate < prescriptionDto.Date)
        {
            throw new Exception("DueDate cannot be earlier than Date.");
        }

        var prescription = new Prescription
        {
            Date = prescriptionDto.Date,
            DueDate = prescriptionDto.DueDate,
            Patient = patient,
            IdDoctor = prescriptionDto.Doctor.IdDoctor,
            PrescriptionMedicaments = prescriptionDto.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Details
            }).ToList()
        };

        await _repository.AddPrescriptionAsync(prescription);
    }

    public async Task<PatientDto> GetPatientWithPrescriptionsAsync(int patientId)
    {
        var patient = await _repository.GetPatientWithPrescriptionsAsync(patientId);
        if (patient == null)
        {
            return null;
        }

        return new PatientDto
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.Birthdate,
            Prescriptions = patient.Prescriptions.Select(p => new PrescriptionDto
            {
                IdPrescription = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,
                Doctor = new DoctorDto
                {
                    IdDoctor = p.Doctor.IdDoctor,
                    FirstName = p.Doctor.FirstName,
                    LastName = p.Doctor.LastName,
                    Email = p.Doctor.Email
                },
                Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentDto
                {
                    IdMedicament = pm.Medicament.IdMedicament,
                    Name = pm.Medicament.Name,
                    Description = pm.Medicament.Description,
                    Type = pm.Medicament.Type,
                    Dose = pm.Dose,
                    Details = pm.Details
                }).ToList()
            }).OrderBy(p => p.DueDate).ToList()
        };
    }
}
