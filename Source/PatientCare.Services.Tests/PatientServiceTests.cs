using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PatientCare.Repository.Interfaces;
using PatientCare.Repository.Models;
using PatientCare.Services.DTO;
using System;
using System.Collections.Generic;

namespace PatientCare.Services.Tests
{
    [TestClass]
    public class PatientServiceTests
    {
        Mock<IMapper> mapper;
        Mock<IPatientRepository> patientRepository;
        Mock<IImmunisationRepository> immunisationRepository;

        private PatientService GetService()
        {
            mapper = new Mock<IMapper>();
            patientRepository = new Mock<IPatientRepository>();
            immunisationRepository = new Mock<IImmunisationRepository>();

            return new PatientService(mapper.Object, patientRepository.Object, immunisationRepository.Object);
        }

        [TestMethod]
        public void CreatePatient_Return_Exception_When_PatientIsNull()
        {
            PatientDto patient = null;

            var service = GetService();

            Assert.ThrowsException<ArgumentNullException>(() => service.CreatePatient(patient));
        }

        [TestMethod]
        public void CreatePatient_Return_Success()
        {
            PatientDto patient = new PatientDto(DateTime.Now);

            var service = GetService();

            service.CreatePatient(patient);

            patientRepository.Verify(x => x.CreatePatient(It.IsAny<Patient>()), Times.Once);
        }

        [TestMethod]
        public void CreatePatient_Return_Success_With_Immunisation()
        {
            PatientDto patient = new PatientDto(DateTime.Now)
            {
                Immunisations = new List<ImmunisationDto>()
                {
                    new ImmunisationDto(),
                    new ImmunisationDto()
                }
            };

            var service = GetService();

            service.CreatePatient(patient);

            patientRepository.Verify(x => x.CreatePatient(It.IsAny<Patient>()), Times.Once);
            immunisationRepository.Verify(x => x.Add(It.IsAny<int>(), It.IsAny<Immunisation>()), Times.Exactly(2));
        }

        [TestMethod]
        public void AddImmunisation_Return_Success()
        {
            int patientId = 100;
            var immunisation = new ImmunisationDto();

            var service = GetService();

            service.AddImmunisation(patientId, immunisation);

            immunisationRepository.Verify(x => x.Add(It.IsAny<int>(), It.IsAny<Immunisation>()), Times.Once);
        }

        [TestMethod]
        public void GetImmunisationCount_Return_Success()
        {
            int patientId = 100;
            var immunisation = new ImmunisationDto();
            var patient = new PatientDto(DateTime.Now) { Id = 100, Immunisations = new List<ImmunisationDto>() { new ImmunisationDto() } };
            
            var service = GetService();

            immunisationRepository.Setup(x => x.GetTotal(It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(patient.Immunisations.Count);

            service.CreatePatient(patient);

            var count = service.GetImmunisationCount(patientId);

            immunisationRepository.Verify(x => x.GetTotal(It.IsAny<int>(), It.IsAny<DateTime>()), Times.Once);

            Assert.AreEqual(count, patient.Immunisations.Count);
        }

        [TestMethod]
        public void GetImmunisation_Return_Success()
        {
            int patientId = 100;
            int immunisationId = 10;

            var service = GetService();

            service.GetImmunisation(patientId, immunisationId);

            immunisationRepository.Verify(x => x.Get(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void Merge_Return_Exception()
        {
            var service = GetService();

            Assert.ThrowsException<ArgumentNullException>(() => service.Merge(null, new PatientDto(DateTime.Now)));
        }

        [TestMethod]
        public void Merge_Return_Success()
        {
            var source = new PatientDto(DateTime.Now);
            var destinition = new PatientDto(DateTime.Now);

            var service = GetService();

            service.Merge(source, destinition);

            immunisationRepository.Verify(x => x.Merge(It.IsAny<int>(), It.IsAny<List<Immunisation>>()), Times.Once);
        }

        [TestMethod]
        public void RemoveImmunisation_Return_Success()
        {
            var source = new PatientDto(DateTime.Now);
            var destinition = new PatientDto(DateTime.Now);

            var service = GetService();

            service.RemoveImmunisation(100, 10);

            immunisationRepository.Verify(x => x.Remove(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
    }
}
