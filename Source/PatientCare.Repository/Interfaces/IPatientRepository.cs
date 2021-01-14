using PatientCare.Repository.Models;
using System;

namespace PatientCare.Repository.Interfaces
{
    public interface IPatientRepository
    {
        void CreatePatient(Models.Patient patient);
    }
}