using Microsoft.EntityFrameworkCore;
using RestAPI_Zad_9.Models;

namespace RestAPI_Zad_9.Repos
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly s24412Context _context;

        public PrescriptionRepository(s24412Context context)
        {
            _context = context;
        }

        public async Task AddPrescriptionAsync(Prescription prescription)
        {
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
        }

        public async Task<Patient> GetPatientWithPrescriptionsAsync(int patientId)
        {
            return await _context.Patients
                .Include(p => p.Prescriptions)
                    .ThenInclude(pr => pr.PrescriptionMedicaments)
                        .ThenInclude(pm => pm.Medicament)
                .Include(p => p.Prescriptions)
                    .ThenInclude(pr => pr.Doctor)
                .FirstOrDefaultAsync(p => p.IdPatient == patientId);
        }


        public async Task<Medicament> GetMedicamentByIdAsync(int medicamentId)
        {
            return await _context.Medicaments.FindAsync(medicamentId);
        }
    }

}
