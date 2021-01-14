using System;

namespace PatientCare.Repository.Models
{
    public class Immunisation
    {
        /// <summary>
        /// Must be unique for Patient
        /// </summary>
        public int ImmunisationId { get; set; }

        // public int PatientId { get; set; }

        public string Vaccine { get; set; }

        public int? Outcome { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
