﻿namespace RestAPI_Zad_9.Models.DTOs
{
    public class PatientDto
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<PrescriptionDto> Prescriptions { get; set; }
    }
}
