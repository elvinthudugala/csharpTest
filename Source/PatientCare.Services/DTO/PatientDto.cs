using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientCare.Services.DTO
{
    public class PatientDto
    {
        public List<ImmunisationDto> Immunisations { get; set; } = new List<ImmunisationDto>();

        /// <summary>
        /// Must be unique
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Must not be allowed to change.
        /// </summary>
        public DateTime CreatedDate { get; private set; }

        public PatientDto(DateTime createDate)
        {
            this.CreatedDate = createDate;
        }

        /// <summary>
        /// Creates a deep clone of the current Patient (all fields and properties)
        /// </summary>
        public PatientDto Clone()
        {
            var patienObj = (PatientDto)this.MemberwiseClone();
            patienObj.Id = this.Id;
            patienObj.CreatedDate = this.CreatedDate;
            patienObj.Immunisations = this.Immunisations.Select(x => x.CreateDeepCopy()).ToList();

            return patienObj;
        }

        /// <summary>
        /// Outputs string containing the following (replace [] with actual values):
        /// Id: [Id], CreatedDate: [DD/MM/YYYY], ImmunisationListCount: [Number of items in ImmunisationList] 
        /// </summary>
        public override string ToString()
        {
            return $"Id: {this.Id}, CreatedDate: {this.CreatedDate.ToString("dd/MM/yyyy")}, ImmunisationListCount: {this.Immunisations.Count}";
        }
    }
}
