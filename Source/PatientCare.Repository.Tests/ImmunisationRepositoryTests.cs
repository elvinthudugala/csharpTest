using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PatientCare.Repository.Interfaces;
using PatientCare.Repository.Models;
using System;
using System.Collections.Generic;

namespace PatientCare.Repository.Tests
{
    [TestClass]
    public class ImmunisationRepositoryTests
    {

        public ImmunisationRepositoryTests()
        {
        }

        [TestMethod]
        public void Add_Returns_Fail_When_NullImmunisation()
        {
            var repository = new ImmunisationRepository();

            Assert.ThrowsException<ArgumentNullException>(() => repository.Add(10, null));
        }

        [TestMethod]
        public void Add_Returns_Fail_When_NullPatientId()
        {
            Immunisation immunisation = new Immunisation();
            var repository = new ImmunisationRepository();

            Assert.ThrowsException<ArgumentException>(() => repository.Add(0, immunisation));
        }

        [TestMethod]
        public void Add_Returns_Success_When_EmptyImmunisation()
        {
            var patientId = 100;
            Immunisation immunisation = new Immunisation() { 
                ImmunisationId = 10,
                
            };
            var repository = new ImmunisationRepository();

            repository.Add(patientId, immunisation);

            var returnImmunisation = repository.Get(patientId, immunisation.ImmunisationId);
            Assert.IsNotNull(returnImmunisation);
        }

        [TestMethod]
        public void Add_Returns_DuplicateException()
        {
            var patientId = 100;
            Immunisation immunisation = new Immunisation()
            {
                ImmunisationId = 10,

            };
            var repository = new ImmunisationRepository();

            repository.Add(patientId, immunisation);

            Assert.ThrowsException<System.Data.Linq.DuplicateKeyException>(() => repository.Add(patientId, immunisation));
        }

        [TestMethod]
        public void Merge_Returns_Success_When_NoPriorImmunisation()
        {
            var patientId = 100;
            Immunisation immunisation = new Immunisation()
            {
                ImmunisationId = 10,
            };
            var repository = new ImmunisationRepository();

            repository.Add(patientId, immunisation);
            repository.Remove(patientId, 10);

            List<Immunisation> immunisationsToBeMerged = new List<Immunisation>()
            {
                new Immunisation() { ImmunisationId = 11 },
                new Immunisation() { ImmunisationId = 12 }
            };

            repository.Merge(patientId, immunisationsToBeMerged);
            Assert.IsNull(repository.Get(patientId, 10));
            Assert.IsNotNull(repository.Get(patientId, 11));
            Assert.IsNotNull(repository.Get(patientId, 12));
            //Assert.ThrowsException<System.Data.Linq.DuplicateKeyException>(() => repository.Add(patientId, immunisation));
        }

        [TestMethod]
        public void Merge_Returns_Exception_When_NullPatientId()
        {
            var patientId = 0;
            List<Immunisation> immunisations = new List<Immunisation>();

            var repository = new ImmunisationRepository();

            Assert.ThrowsException<ArgumentException>(() => repository.Merge(patientId, immunisations));
        }

        [TestMethod]
        public void Merge_Returns_Success()
        {
            var patientId = 100;
            List<Immunisation> immunisations = new List<Immunisation>()
            {
                new Immunisation() { ImmunisationId = 10 },
                new Immunisation() { ImmunisationId = 11 }
            };

            var repository = new ImmunisationRepository();

            repository.Add(patientId, new Immunisation() { ImmunisationId = 12 });

            repository.Merge(patientId, immunisations);

            Assert.IsNotNull(repository.Get(patientId, 10));
            Assert.IsNotNull(repository.Get(patientId, 11));
            Assert.IsNotNull(repository.Get(patientId, 12));
        }

        [TestMethod]
        public void Merge_Returns_DuplicateException()
        {
            var patientId = 100;
            List<Immunisation> immunisations = new List<Immunisation>()
            {
                new Immunisation() { ImmunisationId = 10 },
                new Immunisation() { ImmunisationId = 10 }
            };

            var repository = new ImmunisationRepository();

            repository.Add(patientId, new Immunisation() { ImmunisationId = 12 });

            Assert.ThrowsException< System.Data.Linq.DuplicateKeyException>(() => repository.Merge(patientId, immunisations));
        }

        [TestMethod]
        public void Get_Returns_Exception_When_NullPatientId()
        {
            var patientId = 0;
            var immunisationId = 10;
            List<Immunisation> immunisations = new List<Immunisation>();

            var repository = new ImmunisationRepository();

            Assert.ThrowsException<ArgumentException>(() => repository.Get(patientId, immunisationId));
        }

        [TestMethod]
        public void Get_Returns_Exception_When_NullImmunisationId()
        {
            var patientId = 10;
            var immunisationId = 0;
            List<Immunisation> immunisations = new List<Immunisation>();

            var repository = new ImmunisationRepository();

            Assert.ThrowsException<ArgumentException>(() => repository.Get(patientId, immunisationId));
        }

        [TestMethod]
        public void Get_Returns_Null_When_Not_Found()
        {
            var patientId = 100;
            var immunisationId = 10;

            var repository = new ImmunisationRepository();

            var result = repository.Get(patientId, immunisationId);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Get_Returns_Success()
        {
            var patientId = 100;
            var immunisationId = 10;
            
            var repository = new ImmunisationRepository();

            repository.Add(100, new Immunisation() { ImmunisationId = 10 });

            var result = repository.Get(patientId, immunisationId);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTotal_Return_Null_When_LatestData()
        {
            var patientId = 100;
            
            var repository = new ImmunisationRepository();

            repository.Add(100, new Immunisation() { ImmunisationId = 10, CreatedDate = DateTime.Now });

            var result = repository.GetTotal(patientId, DateTime.Now.AddDays(-1));
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetTotal_Return_Data()
        {
            var patientId = 100;

            var repository = new ImmunisationRepository();

            repository.Add(100, new Immunisation() { ImmunisationId = 10, CreatedDate = DateTime.Now.AddDays(-30) });

            var result = repository.GetTotal(patientId, DateTime.Now);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Remove_Return_NoChange()
        {
            var patientId = 100;

            var repository = new ImmunisationRepository();

            repository.Add(patientId, new Immunisation() { ImmunisationId = 10, CreatedDate = DateTime.Now.AddDays(-30) });

            repository.Remove(patientId, 11);

            var result = repository.GetTotal(patientId, DateTime.Now);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Remove_Return_Success()
        {
            var patientId = 100;

            var repository = new ImmunisationRepository();

            repository.Add(patientId, new Immunisation() { ImmunisationId = 10, CreatedDate = DateTime.Now.AddDays(-30) });

            repository.Remove(patientId, 10);

            var result = repository.GetTotal(patientId, DateTime.Now);
            Assert.AreEqual(0, result);
        }
    }
}
