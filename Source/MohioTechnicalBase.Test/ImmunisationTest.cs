using MohioTechnicalBase.Core;
using NUnit.Framework;
using System;

namespace MohioTechnicalBase.Test
{
    public class ImmunisationTest
    {
        [Test]
        public void Constructor()
        {
            var vaccine = "Vaccine Test 123";
            var outcome = Outcome.Given;
            var createdDate = DateTime.Now;
            var patientId = Guid.NewGuid().ToString();

            var immunisation = new Immunisation(patientId, vaccine, outcome, createdDate);

            Assert.IsTrue(immunisation.PatientId == patientId);
            Assert.IsTrue(immunisation.VaccineName == vaccine);
            Assert.IsTrue(immunisation.Outcome == outcome);
            Assert.IsTrue(immunisation.ApplicationDate == createdDate);
        }

        [Test]
        public void Clone()
        {
            var vaccine = "Vaccine Test 123";
            var outcome = Outcome.Given;
            var createdDate = DateTime.Now;
            var patientId = Guid.NewGuid().ToString();

            var immunisation1 = new Immunisation(patientId, vaccine, outcome, createdDate);
            var immunisation2 = immunisation1.Clone();

            Assert.IsTrue(immunisation1.PatientId == immunisation2.PatientId);
            Assert.IsTrue(immunisation1.VaccineName == immunisation2.VaccineName);
            Assert.IsTrue(immunisation1.Outcome == immunisation2.Outcome);
            Assert.IsTrue(immunisation1.ApplicationDate == immunisation2.ApplicationDate);
        }
    }
}