using RestAPI_Zad_9.Models.DTOs;

namespace RestAPI_Zad_9.Services
{
    public interface IPrescriptionService
    {
        Task AddPrescriptionAsync(PrescriptionDto prescriptionDto);
        Task<PatientDto> GetPatientWithPrescriptionsAsync(int patientId);
    }
}
