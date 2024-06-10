using RestAPI_Zad_9.Models;

namespace RestAPI_Zad_9.Repos
{
    public interface IPrescriptionRepository
    {
        Task AddPrescriptionAsync(Prescription prescription);
        Task<Patient> GetPatientWithPrescriptionsAsync(int patientId);
        Task<Medicament> GetMedicamentByIdAsync(int medicamentId);
    }

}
