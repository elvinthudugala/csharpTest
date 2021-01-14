using System;

namespace PatientCare.Repository.Models
{
    public class Patient
    {
        /// <summary>
        /// Must be unique
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Must not be allowed to change.
        /// </summary>
        public DateTime CreatedDate { get; private set; }

        public Patient(DateTime createDate)
        {
            this.CreatedDate = createDate;
        }
    }
}
