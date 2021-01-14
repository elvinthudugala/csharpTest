using AutoMapper;
using PatientCare.Repository.Interfaces;
using PatientCare.Repository.Models;
using PatientCare.Services.DTO;
using PatientCare.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace PatientCare.Services
{
    public class PatientService : IPatientService
    {
        private readonly IMapper mapper;
        private readonly IPatientRepository patientRepository;
        private readonly IImmunisationRepository immunisationRepository;

        public PatientService(IMapper mapper, IPatientRepository patientRepository, IImmunisationRepository immunisationRepository)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            this.immunisationRepository = immunisationRepository ?? throw new ArgumentNullException(nameof(immunisationRepository));
        }

        public void CreatePatient(PatientDto patient)
        {
            if (patient == null)
            {
                throw new ArgumentNullException(nameof(patient));
            }

            patientRepository.CreatePatient(mapper.Map<Patient>(patient));

            foreach (var immunisation in patient.Immunisations)
                AddImmunisation(patient.Id, immunisation);
        }

        public void AddImmunisation(int patientId, ImmunisationDto immunisation)
        {
            immunisationRepository.Add(patientId, mapper.Map<Immunisation>(immunisation));
        }

        /// <summary>
        /// The total count the Immunisation Given 1 month before
        /// </summary>
        public long GetImmunisationCount(int patientId)
        {
            var immunisationOlderThan = DateTime.Now.AddMonths(-1);

            return immunisationRepository.GetTotal(patientId, immunisationOlderThan);
        }

        public ImmunisationDto GetImmunisation(int patientId, int immunisationId)
        {
            return this.mapper.Map<ImmunisationDto>(immunisationRepository.Get(patientId, immunisationId));
        }

        /// <summary>
        /// Appends the ImmunisationList from the source to the current patient, immunisationId must be unique
        /// </summary>
        /// <param name="sourcePatient">patient to merge from</param>
        public void Merge(PatientDto sourcePatient, PatientDto destinationPatient)
        {
            if (sourcePatient == null)
            {
                throw new ArgumentNullException(nameof(sourcePatient));
            }

            immunisationRepository.Merge(sourcePatient.Id, this.mapper.Map<List<Immunisation>>(destinationPatient.Immunisations));
        }

        public void RemoveImmunisation(int patientId, int immunisationId)
        {
            immunisationRepository.Remove(patientId, immunisationId);
        }
    }
}
