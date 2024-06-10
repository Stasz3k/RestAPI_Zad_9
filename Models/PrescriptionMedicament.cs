using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI_Zad_9.Models
{
    public class PrescriptionMedicament
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Prescription")]
        public int IdPrescription { get; set; }
        public Prescription Prescription { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Medicament")]
        public int IdMedicament { get; set; }
        public Medicament Medicament { get; set; }

        [Required]
        public int Dose { get; set; }

        [Required, MaxLength(100)]
        public string Details { get; set; }
    }
}
