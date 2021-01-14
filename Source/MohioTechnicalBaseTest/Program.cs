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

using Microsoft.Extensions.DependencyInjection;
using PatientCare.Services.DTO;
using PatientCare.Services.Enums;
using PatientCare.Services.Interfaces;
using System;

namespace MohioTechnicalBaseTest
{
    class Program
    {
        private static ServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            // Add dependencies          
            var services = new ServiceCollection();

            PatientCare.Services.BootStrap.SetupDependencies(services);

            serviceProvider = services.BuildServiceProvider();

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
            var patient = new PatientDto(DateTime.Now)
            {
                Id = 100
            };

            patient.Immunisations.Add(new ImmunisationDto
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            });

            // initialise services
            var patientService = serviceProvider.GetService<IPatientService>();
            patientService.CreatePatient(patient);

            Console.WriteLine(patientService.GetImmunisationCount(patient.Id));
        }

        private static void CreatePatientWithMultipleImmunisation()
        {
            var patient = new PatientDto(DateTime.Now)
            {
                Id = 100
            };

            patient.Immunisations.Add(new ImmunisationDto
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            });

            patient.Immunisations.Add(new ImmunisationDto
            {
                ImmunisationId = 11,
                Vaccine = "Flu 65+",
                Outcome = Outcome.NonResponder,
                CreatedDate = DateTime.Now
            });

            patient.Immunisations.Add(new ImmunisationDto
            {
                ImmunisationId = 12,
                Vaccine = "Flu Vaccine PHO",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now.AddMonths(-2)
            });

            // initialise services
            var patientService = serviceProvider.GetService<IPatientService>();
            patientService.CreatePatient(patient);

            Console.WriteLine(patientService.GetImmunisationCount(patient.Id));
        }

        private static void RemoveImmunisation()
        {
            var patient = new PatientDto(DateTime.Now)
            {
                Id = 100
            };

            patient.Immunisations.Add(new ImmunisationDto
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            });

            patient.Immunisations.Add(new ImmunisationDto
            {
                ImmunisationId = 11,
                Vaccine = "Flu 65+",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now.AddDays(-15)
            });

            patient.Immunisations.Add(new ImmunisationDto
            {
                ImmunisationId = 12,
                Vaccine = "Flu Vaccine PHO",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now.AddMonths(-2)
            });

            // initialise services
            var patientService = serviceProvider.GetService<IPatientService>();
            patientService.CreatePatient(patient);

            patientService.RemoveImmunisation(patient.Id, 10);
            Console.WriteLine(patientService.GetImmunisationCount(patient.Id));
        }

        private static void MergePatients()
        {
            var patient1 = new PatientDto(DateTime.Now)
            {
                Id = 100
            };

            patient1.Immunisations.Add(new ImmunisationDto
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            });

            var patient2 = new PatientDto(DateTime.Now)
            {
                Id = 100
            };

            patient2.Immunisations.Add(new ImmunisationDto
            {
                ImmunisationId = 10,
                Vaccine = "Flu 65+",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now.AddDays(-15)
            });

            // initialise services
            var patientService = serviceProvider.GetService<IPatientService>();

            patientService.CreatePatient(patient1);
            patientService.Merge(patient1, patient2);
            Console.WriteLine(patientService.GetImmunisationCount(patient1.Id));
        }

        private static void ClonePatient()
        {
            var patient1 = new PatientDto(DateTime.Now)
            {
                Id = 100
            };

            patient1.Immunisations.Add(new ImmunisationDto
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            });

            var patient2 = patient1.Clone();

            patient2.Immunisations[0].Outcome = Outcome.AlternativeGiven;

            var result = patient1.Immunisations[0].Outcome != patient2.Immunisations[0].Outcome;

            Console.WriteLine($"Both patients have different outcome: {result}");
        }

        private static void PatientToString()
        {
            var patient = new PatientDto(DateTime.Now)
            {
                Id = 100
            };

            patient.Immunisations.Add(new ImmunisationDto
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            });

            patient.Immunisations.Add(new ImmunisationDto
            {
                ImmunisationId = 10,
                Vaccine = "Flu Vaccine PHO",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now.AddMonths(-2)
            });

            Console.WriteLine(patient.ToString());
        }
    }
}
