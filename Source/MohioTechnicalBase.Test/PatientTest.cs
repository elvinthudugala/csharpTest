using MohioTechnicalBase.Core;
using NUnit.Framework;
using System;
using System.ComponentModel;

namespace MohioTechnicalBase.Test
{
    public class PatientTest
    {
        [Test]
        public void Constructor()
        {
            var patient = new Patient();

            Assert.IsTrue(!string.IsNullOrEmpty(patient.Id));
            Assert.IsTrue(patient.CreatedDate != null);
        }

        [Test]
        public void ReadOnlyDate()
        {
            var patient = new Patient();
            AttributeCollection attributes = TypeDescriptor.GetProperties(patient)["CreatedDate"].Attributes;

            Assert.IsTrue(attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes));
        }

        [Test]
        public void Add()
        {
            var patient = new Patient();
            var vaccineName1 = "Vaccine Test 123";
            var outcome1 = Outcome.Given;
            var vaccineApplicationDate = DateTime.Now;
            var vaccineName2 = "Other Vaccine";
            var outcome2 = Outcome.AlternativeGiven;
            var outcome3 = Outcome.NonResponder;

            patient.Add(new Immunisation(patient.Id, vaccineName1, outcome1, vaccineApplicationDate));
            patient.Add(new Immunisation(patient.Id, vaccineName1, outcome1, vaccineApplicationDate));
            patient.Add(new Immunisation(Guid.NewGuid().ToString(), vaccineName2, outcome2, vaccineApplicationDate.AddMonths(-1)));
            patient.Add(new Immunisation(Guid.NewGuid().ToString(), vaccineName2, outcome3, vaccineApplicationDate.AddMonths(-2)));

            Assert.AreEqual(3, patient.Immunisations.Count);
            Assert.AreEqual(patient.Id, patient.Immunisations[0].PatientId);
            Assert.AreEqual(vaccineName1, patient.Immunisations[0].VaccineName);
            Assert.AreEqual(outcome1, patient.Immunisations[0].Outcome);
            Assert.AreEqual(vaccineApplicationDate, patient.Immunisations[0].ApplicationDate);

            Assert.AreEqual(patient.Id, patient.Immunisations[1].PatientId);
            Assert.AreEqual(vaccineName2, patient.Immunisations[1].VaccineName);
            Assert.AreEqual(outcome2, patient.Immunisations[1].Outcome);
            Assert.AreEqual(vaccineApplicationDate.AddMonths(-1), patient.Immunisations[1].ApplicationDate);

            Assert.AreEqual(patient.Id, patient.Immunisations[2].PatientId);
            Assert.AreEqual(vaccineName2, patient.Immunisations[2].VaccineName);
            Assert.AreEqual(outcome3, patient.Immunisations[2].Outcome);
            Assert.AreEqual(vaccineApplicationDate.AddMonths(-2), patient.Immunisations[2].ApplicationDate);
        }

        [Test]
        public void GetImmunisationsByNameEmptyResult()
        {
            var patient = new Patient();
            var vaccineName = "Vaccine Test 123";

            patient.Add(new Immunisation(patient.Id, vaccineName, Outcome.Given, DateTime.Now));

            Assert.AreEqual(1, patient.Immunisations.Count);
            Assert.AreEqual(0, patient.GetImmunisationsByName("Invalid Test").Count);
        }

        [Test]
        public void GetImmunisationsByName()
        {
            var patient = new Patient();
            var vaccineName1 = "Vaccine Test 123";
            var vaccineName2 = "Placebo Test";
            var vaccineName3 = "Other Vaccine";

            patient.Add(new Immunisation(patient.Id, vaccineName1, Outcome.Given, DateTime.Now));
            patient.Add(new Immunisation(patient.Id, vaccineName1, Outcome.AlternativeGiven, DateTime.Now.AddMonths(-1)));
            patient.Add(new Immunisation(patient.Id, vaccineName2, Outcome.AlternativeGiven, DateTime.Now));
            patient.Add(new Immunisation(patient.Id, vaccineName3, Outcome.NonResponder, DateTime.Now.AddMonths(-2)));

            Assert.AreEqual(4, patient.Immunisations.Count);
            Assert.AreEqual(vaccineName1, patient.GetImmunisationsByName(vaccineName1)[0].VaccineName);
            Assert.AreEqual(vaccineName1, patient.GetImmunisationsByName(vaccineName1)[1].VaccineName);
            Assert.AreEqual(vaccineName2, patient.GetImmunisationsByName(vaccineName2)[0].VaccineName);
            Assert.AreEqual(vaccineName3, patient.GetImmunisationsByName(vaccineName3)[0].VaccineName);
        }

        [Test]
        public void GetImmunisationsByApplicationDateEmptyResult()
        {
            var patient = new Patient();
            var vaccineName = "Vaccine Test 123";
            patient.Add(new Immunisation(patient.Id, vaccineName, Outcome.Given, DateTime.Now.AddMonths(-1)));

            Assert.AreEqual(1, patient.Immunisations.Count);
            Assert.AreEqual(0, patient.GetImmunisationsByApplicationDate(DateTime.Now).Count);
        }

        [Test]
        public void GetImmunisationsByApplicationDate()
        {
            var patient = new Patient();
            var vaccineName1 = "Vaccine Test 123";
            var vaccineName2 = "Other Vaccine";
            var vaccineApplicationDate1 = DateTime.Now;
            var vaccineApplicationDate2 = vaccineApplicationDate1.AddMonths(-1);
            var vaccineApplicationDate3 = vaccineApplicationDate1.AddMonths(-2);

            patient.Add(new Immunisation(patient.Id, vaccineName1, Outcome.Given, vaccineApplicationDate1));
            patient.Add(new Immunisation(patient.Id, vaccineName2, Outcome.AlternativeGiven, vaccineApplicationDate2));
            patient.Add(new Immunisation(patient.Id, vaccineName2, Outcome.NonResponder, vaccineApplicationDate3));

            Assert.AreEqual(3, patient.Immunisations.Count);
            Assert.AreEqual(vaccineApplicationDate1, patient.GetImmunisationsByApplicationDate(vaccineApplicationDate1)[0].ApplicationDate);
            Assert.AreEqual(vaccineApplicationDate2, patient.GetImmunisationsByApplicationDate(vaccineApplicationDate2)[0].ApplicationDate);
            Assert.AreEqual(vaccineApplicationDate3, patient.GetImmunisationsByApplicationDate(vaccineApplicationDate3)[0].ApplicationDate);
        }

        [Test]
        public void Remove()
        {
            var patient = new Patient();
            var vaccineName1 = "Vaccine Test 123";
            var vaccineName2 = "Other Vaccine";
            var vaccineApplicationDate = DateTime.Now;

            patient.Add(new Immunisation(patient.Id, vaccineName1, Outcome.Given, vaccineApplicationDate));
            patient.Add(new Immunisation(patient.Id, vaccineName2, Outcome.NonResponder, vaccineApplicationDate));
            patient.Add(new Immunisation(patient.Id, vaccineName2, Outcome.AlternativeGiven, vaccineApplicationDate.AddMonths(-2)));

            Assert.AreEqual(3, patient.Immunisations.Count);

            Assert.IsTrue(patient.Remove(vaccineName1, vaccineApplicationDate));
            Assert.IsFalse(patient.Remove(vaccineName1, vaccineApplicationDate));

            Assert.AreEqual(2, patient.Immunisations.Count);

            Assert.IsTrue(patient.Remove(vaccineName2, vaccineApplicationDate));
            Assert.IsFalse(patient.Remove(vaccineName2, vaccineApplicationDate));

            Assert.AreEqual(1, patient.Immunisations.Count);

            Assert.IsTrue(patient.Remove(vaccineName2, vaccineApplicationDate.AddMonths(-2)));
            Assert.IsFalse(patient.Remove(vaccineName2, vaccineApplicationDate.AddMonths(-2)));

            Assert.AreEqual(0, patient.Immunisations.Count);
        }

        [Test]
        public void GetTotalGivenWithinLastMonth()
        {
            var patient = new Patient();
            var vaccineName1 = "Vaccine Test 123";
            var vaccineName2 = "Other Vaccine";

            patient.Add(new Immunisation(patient.Id, vaccineName1, Outcome.Given, DateTime.Now));
            patient.Add(new Immunisation(patient.Id, vaccineName2, Outcome.NonResponder, DateTime.Now));
            patient.Add(new Immunisation(patient.Id, vaccineName2, Outcome.AlternativeGiven, DateTime.Now));
            patient.Add(new Immunisation(patient.Id, vaccineName1, Outcome.Given, DateTime.Now.AddMonths(-1)));
            patient.Add(new Immunisation(patient.Id, vaccineName1, Outcome.Given, DateTime.Now.AddMonths(-1).AddDays(1)));
            patient.Add(new Immunisation(patient.Id, vaccineName2, Outcome.Given, DateTime.Now.AddMonths(-1).AddDays(-1)));

            Assert.AreEqual(3, patient.GetTotalGivenWithinLastMonth());
        }

        [Test]
        public void Merge()
        {
            var patient1 = new Patient();
            var vaccineName1 = "Vaccine Test 123";
            var outcome1 = Outcome.Given;
            var vaccineApplicationDate1 = DateTime.Now.AddYears(-1);
            patient1.Add(new Immunisation(patient1.Id, vaccineName1, outcome1, vaccineApplicationDate1));

            var patient2 = new Patient();
            var vaccineName2 = "Other Vaccine";
            var outcome2 = Outcome.AlternativeGiven;
            var vaccineApplicationDate2 = DateTime.Now;
            patient2.Add(new Immunisation(Guid.NewGuid().ToString(), vaccineName2, outcome2, vaccineApplicationDate2));

            var patient3 = new Patient();
            var vaccineName3 = "Placebo Test";
            var outcome3 = Outcome.NonResponder;
            var vaccineApplicationDate3 = DateTime.Now.AddMonths(-3);
            patient3.Add(new Immunisation(Guid.NewGuid().ToString(), vaccineName3, outcome3, vaccineApplicationDate3));

            Assert.AreEqual(1, patient1.Immunisations.Count);

            patient1.Merge(patient2);

            Assert.AreEqual(2, patient1.Immunisations.Count);

            patient1.Merge(patient3);

            Assert.AreEqual(3, patient1.Immunisations.Count);

            Assert.AreEqual(vaccineName1, patient1.Immunisations[0].VaccineName);
            Assert.AreEqual(outcome1, patient1.Immunisations[0].Outcome);
            Assert.AreEqual(vaccineApplicationDate1, patient1.Immunisations[0].ApplicationDate);
            Assert.AreEqual(patient1.Id, patient1.Immunisations[0].PatientId);

            Assert.AreEqual(vaccineName2, patient1.Immunisations[1].VaccineName);
            Assert.AreEqual(outcome2, patient1.Immunisations[1].Outcome);
            Assert.AreEqual(vaccineApplicationDate2, patient1.Immunisations[1].ApplicationDate);
            Assert.AreEqual(patient1.Id, patient1.Immunisations[1].PatientId);

            Assert.AreEqual(vaccineName3, patient1.Immunisations[2].VaccineName);
            Assert.AreEqual(outcome3, patient1.Immunisations[2].Outcome);
            Assert.AreEqual(vaccineApplicationDate3, patient1.Immunisations[2].ApplicationDate);
            Assert.AreEqual(patient1.Id, patient1.Immunisations[2].PatientId);
        }

        [Test]
        public void Clone()
        {
            var patient1 = new Patient();
            var vaccineName1 = "Vaccine Test 123";
            var outcome = Outcome.NonResponder;
            var vaccineApplicationDate = DateTime.Now;

            patient1.Add(new Immunisation(patient1.Id, vaccineName1, outcome, vaccineApplicationDate));
            var patient2 = patient1.Clone();

            Assert.IsTrue(patient1.Id != patient2.Id);
            Assert.AreEqual(vaccineName1, patient2.Immunisations[0].VaccineName);
            Assert.AreEqual(outcome, patient2.Immunisations[0].Outcome);
            Assert.AreEqual(vaccineApplicationDate, patient2.Immunisations[0].ApplicationDate);
        }

        [Test]
        public void ToStringResult()
        {
            var patient = new Patient();
            var vaccineName = "Vaccine Test 123";
            var outcome = Outcome.NonResponder;
            var vaccineApplicationDate = DateTime.Now;

            patient.Add(new Immunisation(patient.Id, vaccineName, outcome, vaccineApplicationDate));

            var result = String.Format("Id: {0}, CreatedDate: {1}, ImmunisationsCount: {2}", patient.Id, vaccineApplicationDate.ToString("dd/MM/yyyy"), patient.Immunisations.Count);

            Assert.AreEqual(result, patient.ToString());
        }
    }
}