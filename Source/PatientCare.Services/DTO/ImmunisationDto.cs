using PatientCare.Services.Enums;
using System;

namespace PatientCare.Services.DTO
{
    public class ImmunisationDto
    {
        /// <summary>
        /// Must be unique for Patient
        /// </summary>
        public int ImmunisationId { get; set; }

        public string Vaccine { get; set; }

        public Outcome? Outcome { get; set; }

        public DateTime CreatedDate { get; set; }

        public ImmunisationDto CreateDeepCopy()
        {
            var immunisiationObj = (ImmunisationDto)this.MemberwiseClone();
            immunisiationObj.ImmunisationId = this.ImmunisationId;
            immunisiationObj.Vaccine = this.Vaccine;
            immunisiationObj.Outcome = this.Outcome;
            immunisiationObj.CreatedDate = this.CreatedDate;

            return immunisiationObj;
        }
    }
}
