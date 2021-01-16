using System;
using System.Collections.Generic;
using MohioTechnicalBaseTest.Business.Model;
using MohioTechnicalBaseTest.Business.ModelExtension;
using MohioTechnicalBaseTest.Common;

namespace MohioTechnicalBaseTest.Application
{


    /// <summary>
    /// This class exposes all the methods available to the UI i.e. in our case Program.cs
    /// </summary>
    public static class PatientImmunisationAPI
    {
        public static void CreatePatientWithOneImmunisation()
        {
            try
            {
                //Enroll new Patient
                var patient = new Patient(100, DateTime.Now);

                //Add Immunisation details to the Patient
                var patientimmunisation = new PatientImmunisation(patient);

                //Create Immunisation Record
                var vaccine = new Immunisation
                {
                    ImmunisationId = 10,
                    Vaccine = "Flu Diabetes",
                    Outcome = Outcome.Given,
                    CreatedDate = DateTime.Now
                };

                patientimmunisation.Add(patient.Id, vaccine);

                Console.WriteLine(patientimmunisation.GetTotal());

            }

            catch (Exception ex)
            {
                Console.WriteLine("CreatePatientWithOneImmunisation: "+ ex.Message);

            }
        }


        public static void CreatePatientWithMultipleImmunisation()
        {

            try
            {
                var patient = new Patient
                {
                    Id = 100,
                    CreatedDate = DateTime.Now
                };

                //Add Immunisation details to the Patient
                var patientimmunisation = new PatientImmunisation(patient);

                // WARNING: Immunisation ID Must be unique for Patient , so if we try adding multiple Immunisation record with Same ID, they will be ignored
                //Create Immunisation Record
                var vaccine1 = new Immunisation
                {
                    ImmunisationId = 10,
                    Vaccine = "Flu Diabetes",
                    Outcome = Outcome.Given,
                    CreatedDate = DateTime.Now
                };

                //Create Immunisation Record
                var vaccine2 = new Immunisation
                {
                    ImmunisationId = 10,
                    Vaccine = "Flu 65+",
                    Outcome = Outcome.NonResponder,
                    CreatedDate = DateTime.Now
                };

                //Create Immunisation Record
                var vaccine3 = new Immunisation
                {
                    ImmunisationId = 10,
                    Vaccine = "Flu Vaccine PHO",
                    Outcome = Outcome.Given,
                    CreatedDate = DateTime.Now.AddMonths(-2)
                };

                patientimmunisation.Add(patient.Id, vaccine1);
                patientimmunisation.Add(patient.Id, vaccine2);
                patientimmunisation.Add(patient.Id, vaccine3);


                Console.WriteLine(patientimmunisation.GetTotal());

            }

            catch (Exception ex)
            {
                Console.WriteLine("CreatePatientWithMultipleImmunisation: " + ex.Message);
            }
        }

        public static void PatientToString()
        {
            try
            {

                var patient = new Patient
                {
                    Id = 100,
                    CreatedDate = DateTime.Now
                };

                //Add Immunisation details to the Patient
                var patientimmunisation = new PatientImmunisation(patient);

                //Create Immunisation Record
                var vaccine1 = new Immunisation
                {
                    ImmunisationId = 10,
                    Vaccine = "Flu Diabetes",
                    Outcome = Outcome.Given,
                    CreatedDate = DateTime.Now
                };

                //Create Immunisation Record
                var vaccine2 = new Immunisation
                {
                    ImmunisationId = 10,
                    Vaccine = "Flu Vaccine PHO",
                    Outcome = Outcome.Given,
                    CreatedDate = DateTime.Now.AddMonths(-2)
                };

                patientimmunisation.Add(patient.Id, vaccine1);
                patientimmunisation.Add(patient.Id, vaccine2);

                Console.WriteLine(patientimmunisation.PrintToString());

            }

            catch (Exception ex)
            {
                Console.WriteLine("PatientToString: "+ ex.Message);


            }
        }


        public static void MergePatients()
        {
            try
            {
                var patient1 = new Patient
                {
                    Id = 100,
                    CreatedDate = DateTime.Now
                };

                var patient1immunisation = new PatientImmunisation(patient1);

                var vaccine1 = new Immunisation
                {
                    ImmunisationId = 10,
                    Vaccine = "Flu Diabetes",
                    Outcome = Outcome.Given,
                    CreatedDate = DateTime.Now
                };

                patient1immunisation.Add(patient1.Id, vaccine1);



                var patient2 = new Patient
                {
                    Id = 100,
                    CreatedDate = DateTime.Now
                };

                var patient2immunisation = new PatientImmunisation(patient2);

                var vaccine2 = new Immunisation
                {
                    ImmunisationId = 10,
                    Vaccine = "Flu 65+",
                    Outcome = Outcome.Given,
                    CreatedDate = DateTime.Now.AddDays(-15)
                };

                patient2immunisation.Add(patient2.Id, vaccine2);

                patient1immunisation.Merge(patient2immunisation);
                Console.WriteLine(patient1immunisation.GetTotal());


            }
            catch (Exception ex)
            {
                Console.WriteLine("MergePatients: "+ ex.Message );


            }
        }


        public static void RemoveImmunisation()
        {

            try
            {
                var patient = new Patient
                {
                    Id = 100,
                    CreatedDate = DateTime.Now
                };


                var patient1immunisation = new PatientImmunisation(patient);

                var vaccine1 = new Immunisation
                {
                    ImmunisationId = 10,
                    Vaccine = "Flu Diabetes",
                    Outcome = Outcome.Given,
                    CreatedDate = DateTime.Now
                };

                patient1immunisation.Add(patient.Id, vaccine1);


                var vaccine2 = new Immunisation
                {
                    ImmunisationId = 10,
                    Vaccine = "Flu 65+",
                    Outcome = Outcome.Given,
                    CreatedDate = DateTime.Now.AddDays(-45)
                };

                patient1immunisation.Add(patient.Id, vaccine2);



                var vaccine3 = new Immunisation
                {
                    ImmunisationId = 10,
                    Vaccine = "Flu Vaccine PHO",
                    Outcome = Outcome.Given,
                    CreatedDate = DateTime.Now.AddMonths(-2)
                };

                patient1immunisation.Add(patient.Id, vaccine3);

                patient1immunisation.Remove(10);

                Console.WriteLine(patient1immunisation.GetTotal());


            }
            catch (Exception ex)
            {
                Console.WriteLine("RemoveImmunisation: "+ex.Message);


            }
        }


        public static void ClonePatient()
        {

            try
            {
                var patient1 = new Patient
                {
                    Id = 100,
                    CreatedDate = DateTime.Now
                };

                var patient1immunisation = new PatientImmunisation(patient1);

                var vaccine1 = new Immunisation
                {
                    ImmunisationId = 10,
                    Vaccine = "Flu Diabetes",
                    Outcome = Outcome.Given,
                    CreatedDate = DateTime.Now
                };

                patient1immunisation.Add(patient1.Id, vaccine1);

                var patient2immunisation = patient1immunisation.CLone();

                patient2immunisation.Get(10).Outcome = Outcome.AlternativeGiven;

                Console.WriteLine(patient1immunisation.GetTotal());
                Console.WriteLine(patient2immunisation.GetTotal());


            }
            catch (Exception ex)
            {
                Console.WriteLine("ClonePatient: "+ex.Message);
                

            }
        }

    }
}
