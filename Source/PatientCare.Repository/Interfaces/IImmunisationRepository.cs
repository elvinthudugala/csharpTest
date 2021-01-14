
using PatientCare.Repository.Models;
using System;
using System.Collections.Generic;

namespace PatientCare.Repository.Interfaces
{
    public interface IImmunisationRepository
    {
        void Add(int patientId, Immunisation immunisation);
        Immunisation Get(int patientId, int immunisationId);
        void Remove(int patientId, int immunisationId);
        long GetTotal(int patientId, DateTime immunisationOlderThan);
        void Merge(int patientId, List<Immunisation> immunisations);
    }
}