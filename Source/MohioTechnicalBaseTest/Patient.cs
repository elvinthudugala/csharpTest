using System;
using System.Collections.Generic;
using System.Linq;

namespace MohioTechnicalBaseTest
{
    public class Patient
    {
        private List<Immunisation> _immunisationList;

        /// <summary>
        /// Must be unique
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Must not be allowed to change.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        public Patient()
        {
            _immunisationList = new List<Immunisation>();
        }
        
        public void Add(Immunisation immunisation)
        {
            //The Id is in-use, generate new Id
            if (_immunisationList.Any(i => i.ImmunisationId == immunisation.ImmunisationId))
                immunisation.ImmunisationId = GetNextAvailableId; 

            _immunisationList.Add(immunisation);
        }

        public Immunisation Get(int immunisationId)
        {
            return _immunisationList.Where(i => i.ImmunisationId == immunisationId).FirstOrDefault();
        }

        public void Remove(int immunisationId)
        {
            _immunisationList.RemoveAll(i => i.ImmunisationId == immunisationId);
        }

        /// <summary>
        /// The total count the Immunisation Given 1 month before
        /// </summary>
        public decimal GetTotal()
        {
            return _immunisationList.Where(i => i.CreatedDate < DateTime.Now.AddMonths(-1)).Count();
        }

        /// <summary>
        /// Appends the ImmunisationList from the source to the current patient, immunisationId must be unique
        /// </summary>
        /// <param name="sourcePatient">patient to merge from</param>
        public void Merge(Patient sourcePatient)
        {
            MergeImmunisationList(sourcePatient._immunisationList);
        }
        
        private void MergeImmunisationList(List<Immunisation> sourceList)
        {
            if (sourceList == null)
                return;

            foreach (Immunisation i in sourceList)
                Add(i);
        }

        /// <summary>
        /// Creates a deep clone of the current Patient (all fields and properties)
        /// </summary>
        public Patient Clone()
        {
            Patient newPatient =  new Patient
            {
                Id = Id,
                CreatedDate = CreatedDate,
            };

            newPatient.MergeImmunisationList((from i in _immunisationList
                                              select new Immunisation {
                                                  ImmunisationId = i.ImmunisationId,
                                                  Vaccine = i.Vaccine,
                                                  Outcome = i.Outcome,
                                                  CreatedDate = i .CreatedDate,
                                              }).ToList());
            return newPatient;
        }

        /// <summary>
        /// Outputs string containing the following (replace [] with actual values):
        /// Id: [Id], CreatedDate: [DD/MM/YYYY], ImmunisationListCount: [Number of items in ImmunisationList] 
        /// </summary>
        public override string ToString()
        {
            //Use ToShortDateString() for datetime to match the computer's culture set up
            return $"Id: {Id}, CreatedDate: {CreatedDate.ToShortDateString()}, ImmunisationListCount: {_immunisationList.Count}";
        }

        private int GetNextAvailableId => !_immunisationList.Any()? 1 : _immunisationList.Max(i => i.ImmunisationId) + 1;
    }
}
