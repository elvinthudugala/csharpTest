using System;

namespace MohioTechnicalBase.Core
{
    public class Immunisation
    {
        /// <summary>
        /// Must be unique for Patient
        /// </summary>
        public string PatientId { get; set; }

        public string VaccineName { get; set; }

        public Outcome? Outcome { get; set; }

        public DateTime ApplicationDate { get; set; }

        public Immunisation(string patientId, string vaccineName, Outcome outcome, DateTime applicationDate)
        {
            PatientId = patientId;
            VaccineName = vaccineName;
            Outcome = outcome;
            ApplicationDate = applicationDate;
        }

        public Immunisation Clone()
        {
            return (Immunisation)this.MemberwiseClone();
        }
    }
}
