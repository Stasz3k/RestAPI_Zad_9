﻿namespace RestAPI_Zad_9.Models.DTOs
{
    public class MedicamentDto
    {
        public int IdMedicament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Dose { get; set; }
        public string Details { get; set; }
    }
}
