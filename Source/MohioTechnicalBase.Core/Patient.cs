using System;
using System.Collections.Generic;
using System.Linq;

namespace MohioTechnicalBase.Core
{
    public class Patient
    {
        public List<Immunisation> Immunisations = new List<Immunisation>();

        /// <summary>
        /// Must be unique
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Must not be allowed to change.
        /// </summary>
        public DateTime CreatedDate { get; }

        public Patient()
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
        }

        public void Add(Immunisation immunisation)
        {
            if (!Immunisations.Any(x => x.VaccineName == immunisation.VaccineName && x.ApplicationDate.Date == immunisation.ApplicationDate.Date))
            {
                var immunisationClone = immunisation.Clone();
                immunisationClone.PatientId = Id;
                Immunisations.Add(immunisationClone);
            }
        }

        public List<Immunisation> GetImmunisationsByName(string vaccineName)
        {
            return Immunisations.FindAll(x => x.VaccineName == vaccineName).ToList();
        }

        public List<Immunisation> GetImmunisationsByApplicationDate(DateTime applicationDate)
        {
            return Immunisations.FindAll(x => x.ApplicationDate.Date == applicationDate.Date).ToList();
        }

        public bool Remove(string vaccineName, DateTime applicationDate)
        {
            var immunisation = Immunisations.FirstOrDefault(x => x.VaccineName == vaccineName && x.ApplicationDate.Date == applicationDate.Date);

            return Immunisations.Remove(immunisation);
        }

        /// <summary>
        /// The total count the Immunisation Status is Given and recorded last month
        /// </summary>
        public int GetTotalGivenWithinLastMonth()
        {
            var result = 0;

            foreach (var item in Immunisations)
            {
                if (item.Outcome == Outcome.Given && (item.ApplicationDate.Date >= DateTime.Now.AddMonths(-1).Date))
                    result++;
            }

            return result;
        }

        /// <summary>
        /// Appends the Immunisations from the source to the current patient, immunisationId must be unique
        /// </summary>
        /// <param name="sourcePatient">patient to merge from</param>
        public void Merge(Patient sourcePatient)
        {
            foreach (var item in sourcePatient.Immunisations)
            {
                if (!Immunisations.Any(x => x.VaccineName == item.VaccineName && x.ApplicationDate.Date == item.ApplicationDate.Date))
                {
                    var immunisationClone = item.Clone();
                    immunisationClone.PatientId = Id;
                    Immunisations.Add(immunisationClone);
                }
            }
        }

        /// <summary>
        /// Creates a deep clone of the current Patient (all fields and properties, except Id)
        /// </summary>
        public Patient Clone()
        {
            var clone = (Patient)this.MemberwiseClone();
            clone.Id = Guid.NewGuid().ToString();

            return clone;
        }

        /// <summary>
        /// Outputs string containing the following (replace [] with actual values):
        /// Id: [Id], CreatedDate: [DD/MM/YYYY], ImmunisationsCount: [Number of items in Immunisations] 
        /// </summary>
        public override string ToString()
        {
            return String.Format("Id: {0}, CreatedDate: {1}, ImmunisationsCount: {2}", Id, CreatedDate.ToString("dd/MM/yyyy"), Immunisations.Count);
        }
    }
}
