/*
    Welcome to the Mohio technical Test 1
    ---------------------------------------------------------------------------------
    This test contains a small patient application that has several issues.

    Please fix them and execute each method below.
	
    Rules
    ---------------------------------------------------------------------------------
    1. The entire solution must be written in C# .net5
    2. Please modify the solution anyway you prefer. 
        a. You can modify classes, rename and add methods, change property types, add projects 
        b. Use libraries or frameworks (must be .net based)
    3. Write tests
 
    Show your skills


    Good luck 

    When you have finished the solution please do a pull request to original repository (push only the necessary files)
*/

using MohioTechnicalBase.Core;
using System;

namespace MohioTechnicalBase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Mohio Technical Test 1");

            CreatePatientWithOneImmunisation();
            CreatePatientWithMultipleImmunisation();
            RemoveImmunisation();
            MergePatients();
            ClonePatient();
            PatientToString();
        }

        private static void CreatePatientWithOneImmunisation()
        {
            var patient = new Patient();
            patient.Add(new Immunisation(patient.Id, "Flu Diabetes", Outcome.Given, DateTime.Now));

            Console.WriteLine(patient.GetTotalGivenWithinLastMonth());
        }

        private static void CreatePatientWithMultipleImmunisation()
        {
            var patient = new Patient();

            patient.Add(new Immunisation(patient.Id, "Flu Diabetes", Outcome.Given, DateTime.Now));
            patient.Add(new Immunisation(patient.Id, "Flu 65+", Outcome.NonResponder, DateTime.Now));
            patient.Add(new Immunisation(patient.Id, "Flu Vaccine PHO", Outcome.Given, DateTime.Now.AddMonths(-2)));

            Console.WriteLine(patient.GetTotalGivenWithinLastMonth());
        }

        private static void RemoveImmunisation()
        {
            var patient = new Patient();
            var vaccineName = "Flu Diabetes";
            var immunisationDate = DateTime.Now;

            patient.Add(new Immunisation(patient.Id, vaccineName, Outcome.Given, immunisationDate));
            patient.Add(new Immunisation(patient.Id, "Flu 65+", Outcome.Given, DateTime.Now.AddDays(-15)));
            patient.Add(new Immunisation(patient.Id, "Flu Vaccine PHO", Outcome.Given, DateTime.Now.AddMonths(-2)));

            patient.Remove(vaccineName, immunisationDate);
            Console.WriteLine(patient.GetTotalGivenWithinLastMonth());
        }

        private static void MergePatients()
        {
            var patient1 = new Patient();
            patient1.Add(new Immunisation(patient1.Id, "Flu Diabetes", Outcome.Given, DateTime.Now));

            var patient2 = new Patient();
            patient2.Add(new Immunisation(patient2.Id, "Flu 65+", Outcome.Given, DateTime.Now.AddDays(-15)));

            var patient3 = new Patient();
            patient3.Add(new Immunisation(patient3.Id, "Flu Vaccine PHO", Outcome.Given, DateTime.Now.AddMonths(-2)));

            patient1.Merge(patient2);
            patient1.Merge(patient3);

            Console.WriteLine(patient1.GetTotalGivenWithinLastMonth());
        }

        private static void ClonePatient()
        {
            var patient1 = new Patient();
            var vaccineName = "Flu Diabetes";
            patient1.Add(new Immunisation(patient1.Id, vaccineName, Outcome.Given, DateTime.Now));

            var patient2 = patient1.Clone();
            patient2.GetImmunisationsByName(vaccineName)[0].Outcome = Outcome.AlternativeGiven;

            Console.WriteLine(patient1.GetTotalGivenWithinLastMonth());
            Console.WriteLine(patient2.GetTotalGivenWithinLastMonth());
        }

        private static void PatientToString()
        {
            var patient = new Patient();

            patient.Add(new Immunisation(patient.Id, "Flu Diabetes", Outcome.Given, DateTime.Now));
            patient.Add(new Immunisation(patient.Id, "Flu Vaccine PHO", Outcome.Given, DateTime.Now.AddMonths(-2)));

            Console.WriteLine(patient.ToString());
        }
    }
}
