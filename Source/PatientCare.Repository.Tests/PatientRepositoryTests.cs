using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PatientCare.Repository.Interfaces;
using PatientCare.Repository.Models;
using System;
using System.Collections.Generic;

namespace PatientCare.Repository.Tests
{
    [TestClass]
    public class PatientRepositoryTests
    {

        [TestMethod]
        public void CreatePatient_Return_Exception_When_PatientIsNull()
        {
            var repository = new PatientRepository();

            Assert.ThrowsException<ArgumentNullException>(() => repository.CreatePatient(null));
        }
    }
}
