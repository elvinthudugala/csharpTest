using PatientCare.Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace PatientCare.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly List<Models.Patient> patients = new List<Models.Patient>();

        public void CreatePatient(Models.Patient patient)
        {
            if (patient == null)
            {
                throw new ArgumentNullException(nameof(patient));
            }

            this.patients.Add(patient);
        }
    }
}
