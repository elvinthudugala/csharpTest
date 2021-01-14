using PatientCare.Repository.Interfaces;
using PatientCare.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientCare.Repository
{
    public class ImmunisationRepository : IImmunisationRepository
    {
        private readonly Dictionary<int, List<Immunisation>> patientImmunisations = new Dictionary<int, List<Immunisation>>();
        public void Add(int patientId, Immunisation immunisation)
        {
            if (immunisation == null)
                throw new ArgumentNullException(nameof(immunisation));

            if (patientId < 1)
                throw new ArgumentException(nameof(patientId));

            // if the immunisation id already exists then an exception will be thrown.
            if (!this.patientImmunisations.TryGetValue(patientId, out var immunisations) && (immunisations == null || !immunisations.Any()))
            {
                this.patientImmunisations[patientId] = new List<Immunisation>();
            }
            else if (this.patientImmunisations[patientId].Any(x => x.ImmunisationId == immunisation.ImmunisationId))
            {
                throw new System.Data.Linq.DuplicateKeyException(immunisation, $"Item already exists with the same immunisation id.");
            }

            this.patientImmunisations[patientId].Add(immunisation);
        }

        public void Merge(int patientId, List<Immunisation> immunisations)
        {
            if (patientId < 1)
            {
                throw new ArgumentException(nameof(patientId));
            }

            if (immunisations == null || !immunisations.Any())
            {
                return;
            }

            if (immunisations.Select(x => x.ImmunisationId).Distinct().Count() != immunisations.Count)
            {
                throw new System.Data.Linq.DuplicateKeyException(immunisations, "Multiple records found with the same immunisation id.");
            }

            if (!this.patientImmunisations.Any(x => x.Key == patientId))
            {
                this.patientImmunisations.Add(patientId, new List<Immunisation>(immunisations));
            }

            if (!this.patientImmunisations.ContainsKey(patientId) 
                || this.patientImmunisations[patientId] == null
                || !this.patientImmunisations[patientId].Any())
            {
                this.patientImmunisations[patientId] = new List<Immunisation>(immunisations);
                return;
            }

            // Existing immunisation objects will the same id will be overwritten with the source item.
            foreach (var immunisation in immunisations)
            {
                var existingItem = this.patientImmunisations[patientId]
                    .FirstOrDefault(x => x.ImmunisationId == immunisation.ImmunisationId);

                if (existingItem != null)
                {
                    this.patientImmunisations[patientId]
                        .Remove(existingItem);
                }
            }

            this.patientImmunisations[patientId].AddRange(immunisations);
        }

        public Immunisation Get(int patientId, int immunisationId)
        {
            if (immunisationId < 1)
                throw new ArgumentException(nameof(immunisationId));

            if (patientId < 1)
                throw new ArgumentException(nameof(patientId));

            this.patientImmunisations.TryGetValue(patientId, out var immunisations);
            return immunisations?.FirstOrDefault(x => x.ImmunisationId == immunisationId);
        }

        public long GetTotal(int patientId, DateTime immunisationOlderThan)
        {
            long totalCount = 0;
            if (this.patientImmunisations.ContainsKey(patientId))
            {
                totalCount = this.patientImmunisations[patientId].LongCount(x => x.CreatedDate <= immunisationOlderThan);
            }

            return totalCount;
        }

        public void Remove(int patientId, int immunisationId)
        {
            var immunisationItem = this.Get(patientId, immunisationId);

            if (immunisationItem != null)
            {
                this.patientImmunisations[patientId].Remove(immunisationItem);
            }
        }
    }
}
