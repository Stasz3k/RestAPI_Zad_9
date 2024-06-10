﻿using System.ComponentModel.DataAnnotations;

namespace RestAPI_Zad_9.Models
{
    public class Medicament
    {
        [Key]
        public int IdMedicament { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Description { get; set; }

        [Required, MaxLength(100)]
        public string Type { get; set; }

        public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    }
}
