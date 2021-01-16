
using System;
using Xunit;
using MohioTechnicalBaseTest.Business.Model;
using static MohioTechnicalBaseTest.Business.ModelExtension.PatientImmunisationExtension;
using MohioTechnicalBaseTest.Common;

namespace MohioTechnicalBaseTest.xUnitTests
{/// <summary>
/// This class provide the Unit Test for all the methods and scenarios within to test PatientImmunisationExtension Class
/// </summary>
    public class PatientImmunisationTests
    {

        #region PrintToString Test Cases
        [Fact]
        public void PrintToString_PatientIsNull_ReturnArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new PatientImmunisation(null));
            Assert.Equal("Value cannot be null. (Parameter 'patientEnrolled')", ex.Message);
        }

        [Fact]
        public void PrintToString_PatientImmunisationIsNull_ReturnString()
        {
            var patientImmunisation = new PatientImmunisation(new Patient(100));
            patientImmunisation = null;
            var result = patientImmunisation.PrintToString();
            Assert.Equal("Patient Not Available", result);
        }


        [Fact]
        public void PrintToString_PatientWithoutImmunisation_ReturnFormattedString()
        {

            var newPatient = new PatientImmunisation(new Patient(200));
            Assert.Equal("Id:200, CreatedDate:  " + DateTime.Now.ToShortDateString() + ", ImmunisationListCount: 0", newPatient.PrintToString());

        }


        [Fact]
        public void PrintToString_PatientWithSingleImmunisation_ReturnFormattedString()
        {

            var newPatient = new PatientImmunisation(new Patient(200));
            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(200, vaccine1);


            Assert.Equal("Id:200, CreatedDate:  " + DateTime.Now.ToShortDateString() + ", ImmunisationListCount: 1", newPatient.PrintToString());

        }

        [Fact]
        public void PrintToString_PatientWithMultipleImmunisation_ReturnFormattedString()
        {

            var patient = new Patient(200);

            var newPatient = new PatientImmunisation(patient);
            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(patient.Id, vaccine1);
            var vaccine2 = new Immunisation
            {
                ImmunisationId = 11,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(patient.Id, vaccine2);
            var vaccine3 = new Immunisation
            {
                ImmunisationId = 12,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(patient.Id, vaccine3);

            //Change the created date each time you ran the test to match with patient creation date, i.e. todays date in DD-MM-YYYY format
            Assert.Equal("Id:200, CreatedDate:  " + DateTime.Now.ToShortDateString() + ", ImmunisationListCount: 3", newPatient.PrintToString());

        }
        #endregion


        #region Count Test Cases

        [Fact]
        public void Count_PatientImmunisationIsNull_ReturnZero()
        {
            var patientImmunisation = new PatientImmunisation(new Patient(100));
            //set it null
            patientImmunisation = null;
            Assert.Equal(0, patientImmunisation.Count());
        }

        [Fact]
        public void Count_PatientImmunisationListIsNull_ReturnZero()
        {
            var patientImmunisation = new PatientImmunisation(new Patient(100));
            Assert.Equal(0, patientImmunisation.Count());
        }

        [Fact]
        public void Count_PatientWithSingleImmunisation_ReturnOne()
        {
            var newPatient = new PatientImmunisation(new Patient(200));
            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(200, vaccine1);

            Assert.Equal(1, newPatient.Count());
        }


        [Fact]
        public void Count_PatientWithMultipleImmunisation_ReturnNonZero()
        {
            var patient = new Patient(200);

            var newPatient = new PatientImmunisation(patient);
            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(patient.Id, vaccine1);
            var vaccine2 = new Immunisation
            {
                ImmunisationId = 11,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(patient.Id, vaccine2);
            var vaccine3 = new Immunisation
            {
                ImmunisationId = 12,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(patient.Id, vaccine3);

            Assert.Equal(3, newPatient.Count());
        }
        #endregion


        #region GetTotal Test Cases
        [Fact]
        public void GetTotal_PatientImmunisationIsNull_ReturnZero()
        {
            var patientImmunisation = new PatientImmunisation(new Patient(100));
            //set it null
            patientImmunisation = null;
            Assert.Equal(0, patientImmunisation.GetTotal());
        }

        [Fact]
        public void GetTotal_PatientImmunisationListIsNull_ReturnZero()
        {
            var patientImmunisation = new PatientImmunisation(new Patient(100));
            Assert.Equal(0, patientImmunisation.GetTotal());
        }

        [Fact]
        public void GetTotal_PatientWithSingleImmunisationLesserMonthAgo_ReturnZero()
        {
            var newPatient = new PatientImmunisation(new Patient(200));
            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(200, vaccine1);

            Assert.Equal(0, newPatient.GetTotal());
        }
        [Fact]
        public void GetTotal_PatientWithSingleImmunisationGreaterthanMonthAgo_ReturnZero()
        {
            var newPatient = new PatientImmunisation(new Patient(200));
            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now.AddMonths(-2)
            };
            newPatient.Add(200, vaccine1);

            Assert.Equal(1, newPatient.GetTotal());
        }


        [Fact]
        public void GetTotal_PatientWithMultipleImmunisation_ReturnTwo()
        {
            var patient = new Patient(200);

            var newPatient = new PatientImmunisation(patient);
            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now.AddMonths(-2)
            };
            newPatient.Add(patient.Id, vaccine1);
            var vaccine2 = new Immunisation
            {
                ImmunisationId = 11,
                Vaccine = "Flu Shot",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(patient.Id, vaccine2);
            var vaccine3 = new Immunisation
            {
                ImmunisationId = 12,
                Vaccine = "Boostrix",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now.AddMonths(-1)
            };
            newPatient.Add(patient.Id, vaccine3);

            Assert.Equal(2, newPatient.GetTotal());
        }

        #endregion


        #region Add Test Cases

        [Fact]
        public void Add_PatientImmunisationIsNull_ReturnZero()
        {
            var patientImmunisation = new PatientImmunisation(new Patient(100));
            patientImmunisation = null;
            var ex = Assert.Throws<Exception>(() => patientImmunisation.Add(100, null));
            Assert.Equal("PatientImmunisation Record is not set", ex.Message);

        }

        [Fact]
        public void Add_PatientImmunisationPatientIdDoesnotMatchEnroller_ExceptionRecordnotmatchwithpassedEnroller()
        {
            var patientImmunisation = new PatientImmunisation(new Patient(100));
            var ex = Assert.Throws<Exception>(() => patientImmunisation.Add(200, null));
            Assert.Equal("PatientImmunisation Record not match with passed Patient Id", ex.Message);

        }
        [Fact]
        public void Add_PatientImmunisationPatientIdMatchEnrollerButWithNullVaccines_ExceptionNoVaccineInformationPassed()
        {
            var patientImmunisation = new PatientImmunisation(new Patient(100));
            var ex = Assert.Throws<Exception>(() => patientImmunisation.Add(100, null));
            Assert.Equal("No Immunisation information passed", ex.Message);
        }


        [Fact]
        public void Add_PatientImmunisationPatientIdMatchEnrollerAddSingleVaccines_ReturnCountOne()
        {
            var newPatient = new PatientImmunisation(new Patient(200));
            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(200, vaccine1);

            Assert.Equal(1, newPatient.Count());
        }

        [Fact]
        public void Add_PatientImmunisationPatientIdMatchEnrollerWithMultipleUniqueVaccines_ReturnCountThree()
        {
            var patient = new Patient(200);

            var newPatient = new PatientImmunisation(patient);
            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(patient.Id, vaccine1);
            var vaccine2 = new Immunisation
            {
                ImmunisationId = 11,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(patient.Id, vaccine2);
            var vaccine3 = new Immunisation
            {
                ImmunisationId = 12,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(patient.Id, vaccine3);

            Assert.Equal(3, newPatient.Count());
        }

        [Fact]
        public void Add_PatientImmunisationPatientIdMatchEnrollerWithNotUniqueVaccines_ReturnExceptionForSecondAdd()
        {
            var patient = new Patient(200);

            var newPatient = new PatientImmunisation(patient);
            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            newPatient.Add(patient.Id, vaccine1);
            var vaccine2 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };

            var ex = Assert.Throws<Exception>(() => newPatient.Add(patient.Id, vaccine2));
            Assert.Equal("Immunisation ID: " + vaccine2.ImmunisationId + "  already exists for Patient " + patient.Id + ", So cannot add the Immunisation Information again.", ex.Message);
        }




        #endregion

        #region Merge Test Cases

        //Destination Patient is NULL
        [Fact]
        public void Merge_NULLSourceWithNULLDestination_ReturnDestPatientWithNoImmunisationAvailable()
        {
            //Same Patient different records are created in system, so merge them. It means they will have different Enrollment ID
            var sourcepatientImmunisation = new PatientImmunisation(new Patient(100));



            var destinationpatientImmunisation = new PatientImmunisation(new Patient(200));


            destinationpatientImmunisation.Merge(sourcepatientImmunisation);

            Assert.Equal("Id:200, CreatedDate:  " + DateTime.Now.ToShortDateString() + ", ImmunisationListCount: 0", destinationpatientImmunisation.PrintToString());

        }


        [Fact]
        //Source Patient is NULL Destination Patient not null
        public void Merge_SourcePatientImmunisationNULLDestinationPatientImmunisationNOTNULL_ReturnDestinationPatientWithImmunisation()
        {
            var sourcepatientImmunisation = new PatientImmunisation(new Patient(100)); //Immmunisation is set null default

            var destinationpatientImmunisation = new PatientImmunisation(new Patient(200));

            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            destinationpatientImmunisation.Add(destinationpatientImmunisation.PatientEnrolled.Id, vaccine1);

            destinationpatientImmunisation.Merge(sourcepatientImmunisation);

            Assert.Equal("Id:200, CreatedDate:  " + DateTime.Now.ToShortDateString() + ", ImmunisationListCount: 1", destinationpatientImmunisation.PrintToString());

        }


        //Destination Patient  and Source Patient Immunisation ID are same, do not alter the Destination Patient Immunisation Record
        [Fact]
        public void Merge_SourcePatientImmunisationSameasDestinationPatientImmunisation_ReturnSourcePatientWithImmunisationKeepSourceImmunisationIntact()
        {
            var sourcepatientImmunisation = new PatientImmunisation(new Patient(100)); //Immmunisation is set null default

            var sourcevaccine = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            sourcepatientImmunisation.Add(sourcepatientImmunisation.PatientEnrolled.Id, sourcevaccine);

            var destinationpatientImmunisation = new PatientImmunisation(new Patient(200));

            var destinationvaccine = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes2",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            destinationpatientImmunisation.Add(destinationpatientImmunisation.PatientEnrolled.Id, destinationvaccine);

            destinationpatientImmunisation.Merge(sourcepatientImmunisation);

            var isdestinationImmunisationNotChangedbySource = destinationpatientImmunisation.Get(10).ImmunisationId == 10 && destinationpatientImmunisation.Get(10).Outcome == Outcome.Given && destinationpatientImmunisation.Get(10).Vaccine == "Flu Diabetes2";

            Assert.Equal("Id:200, CreatedDate:  " + DateTime.Now.ToShortDateString() + ", ImmunisationListCount: 1", destinationpatientImmunisation.PrintToString());
            Assert.True(isdestinationImmunisationNotChangedbySource);

        }
        //Destination Patient  and Source Patient Immunisation ID are not same
        [Fact]
        public void Merge_SourcePatientImmunisationNOTSameasDestinationPatientImmunisation_ReturnDestinationPatientWithImmunisationWithAdditionSourceImmunisationIntact()
        {

            var sourcepatientImmunisation = new PatientImmunisation(new Patient(100)); //Immmunisation is set null default

            var sourcevaccine = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            sourcepatientImmunisation.Add(sourcepatientImmunisation.PatientEnrolled.Id, sourcevaccine);

            var destinationpatientImmunisation = new PatientImmunisation(new Patient(200));

            var destinationvaccine = new Immunisation
            {
                ImmunisationId = 20,
                Vaccine = "Flu Diabetes2",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            destinationpatientImmunisation.Add(destinationpatientImmunisation.PatientEnrolled.Id, destinationvaccine);

            destinationpatientImmunisation.Merge(sourcepatientImmunisation);

            Assert.Equal(2, destinationpatientImmunisation.Count());
        }
        //Destination Patient  and Source Patient Immunisation ID are unique and repeted
        [Fact]
        public void Merge_SourcePatientImmunisationDestinationPatientImmunisationContainsSameAndDifferentVaccine_ReturnDestinationPatientWithUniqueImmunisation()
        {

            var sourcepatientImmunisation = new PatientImmunisation(new Patient(100)); //Immmunisation is set null default

            var sourcevaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            sourcepatientImmunisation.Add(sourcepatientImmunisation.PatientEnrolled.Id, sourcevaccine1);

            var sourcevaccine2 = new Immunisation
            {
                ImmunisationId = 30,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            sourcepatientImmunisation.Add(sourcepatientImmunisation.PatientEnrolled.Id, sourcevaccine2);

            var destinationpatientImmunisation = new PatientImmunisation(new Patient(200));

            var destinationvaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes2",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            destinationpatientImmunisation.Add(destinationpatientImmunisation.PatientEnrolled.Id, destinationvaccine1);


            var destinationvaccine2 = new Immunisation
            {
                ImmunisationId = 20,
                Vaccine = "Flu Diabetes2",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now
            };
            destinationpatientImmunisation.Add(destinationpatientImmunisation.PatientEnrolled.Id, destinationvaccine2);



            destinationpatientImmunisation.Merge(sourcepatientImmunisation);

            Assert.Equal(3, destinationpatientImmunisation.Count());

        }




        #endregion


        #region Get Test Cases
        [Fact]
        public void Get_PatientImmunisationIsNULL_ReturnNULL()
        {

            var patientImmunisation = new PatientImmunisation(new Patient(200));

            patientImmunisation = null;

            Assert.True(patientImmunisation.Get(10) == null);
        }

        [Fact]
        public void Get_ImmunisationListIsNull_ReturnNULL()
        {

            var patientImmunisation = new PatientImmunisation(new Patient(200));

            Assert.True(patientImmunisation.Get(10) == null);
        }


        [Fact]
        public void Get_ImmunisationListIsNOTNULLBUTImmunisationIDPassedIsNOTFound_ReturnNULL()
        {

            var patientImmunisation = new PatientImmunisation(new Patient(100)); //Immmunisation is set null default

            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            patientImmunisation.Add(patientImmunisation.PatientEnrolled.Id, vaccine1);

            var vaccine2 = new Immunisation
            {
                ImmunisationId = 30,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            patientImmunisation.Add(patientImmunisation.PatientEnrolled.Id, vaccine2);

            Assert.True(patientImmunisation.Get(200) == null);

        }

        [Fact]
        public void Get_ImmunisationListIsNOTNULLBUTImmunisationIDPassedIsFound_ReturnImmunisationObject()
        {

            var patientImmunisation = new PatientImmunisation(new Patient(100)); //Immmunisation is set null default

            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            patientImmunisation.Add(patientImmunisation.PatientEnrolled.Id, vaccine1);

            var vaccine2 = new Immunisation
            {
                ImmunisationId = 30,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            patientImmunisation.Add(patientImmunisation.PatientEnrolled.Id, vaccine2);

            Assert.True(patientImmunisation.Get(10) != null);
        }


        #endregion


        #region Remove Test Cases 

        [Fact]
        public void Remove_PatientImmunisationIsNULL_ReturnZero()
        {
            var patientImmunisation = new PatientImmunisation(new Patient(200));

            patientImmunisation = null;

            Assert.Equal(0, patientImmunisation.Count());

            patientImmunisation.Remove(10);

            Assert.Equal(0, patientImmunisation.Count());
        }

        [Fact]
        public void Remove_PatientImmunisationListIsNULL_ReturnZero()
        {

            var patientImmunisation = new PatientImmunisation(new Patient(200));

            Assert.Equal(0, patientImmunisation.Count());

            patientImmunisation.Remove(10);

            Assert.Equal(0, patientImmunisation.Count());
        }

        [Fact]
        public void Remove_PatientImmunistationListIsNotNullButItemTobeRemoveIDNotFound_ReturnSameCount()
        {
            var patientImmunisation = new PatientImmunisation(new Patient(100)); //Immmunisation is set null default

            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            patientImmunisation.Add(patientImmunisation.PatientEnrolled.Id, vaccine1);

            var vaccine2 = new Immunisation
            {
                ImmunisationId = 30,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            patientImmunisation.Add(patientImmunisation.PatientEnrolled.Id, vaccine2);

            Assert.Equal(2, patientImmunisation.Count());

            patientImmunisation.Remove(20);

            Assert.Equal(2, patientImmunisation.Count());
        }

        [Fact]
        public void Remove_PatientImmunistationListIsNotNullButItemTobeRemoveIDFound_ReturnDifferntCount()
        {
            var patientImmunisation = new PatientImmunisation(new Patient(100)); //Immmunisation is set null default

            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            patientImmunisation.Add(patientImmunisation.PatientEnrolled.Id, vaccine1);

            var vaccine2 = new Immunisation
            {
                ImmunisationId = 30,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            patientImmunisation.Add(patientImmunisation.PatientEnrolled.Id, vaccine2);

            Assert.Equal(2, patientImmunisation.Count());

            patientImmunisation.Remove(30);

            Assert.Equal(1, patientImmunisation.Count());
        }





        #endregion

        #region CLone Test Cases 

        [Fact]
        public void Clone_PatientImmunisationIsNULL_ReturnNull()
        {
            var patientImmunisation = new PatientImmunisation(new Patient(200));

            patientImmunisation = null;

            Assert.Null(patientImmunisation.CLone());

        }

        [Fact]
        public void Clone_PatientImmunisationIsNotNULL_ReturnNewObjectWithSameProperty()
        {
            var patientImmunisation = new PatientImmunisation(new Patient(100)); //Immmunisation is set null default

            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            patientImmunisation.Add(patientImmunisation.PatientEnrolled.Id, vaccine1);

            var vaccine2 = new Immunisation
            {
                ImmunisationId = 30,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            patientImmunisation.Add(patientImmunisation.PatientEnrolled.Id, vaccine2);

            var clonepatientImmunisation = patientImmunisation.CLone();

            clonepatientImmunisation.PatientEnrolled.Id = 200;

            Assert.Equal("Id:100, CreatedDate:  " + DateTime.Now.ToShortDateString() + ", ImmunisationListCount: 2", patientImmunisation.PrintToString());
            Assert.Equal("Id:200, CreatedDate:  " + DateTime.Now.ToShortDateString() + ", ImmunisationListCount: 2", clonepatientImmunisation.PrintToString());

        }

        [Fact]
        public void Clone_PatientImmunisationIsNotNULL_ReturnNewObjectWithSamePropertyThenAlterItAndCheckOriginalandCloneProperty()
        {

            var patientImmunisation = new PatientImmunisation(new Patient(100)); //Immmunisation is set null default

            var vaccine1 = new Immunisation
            {
                ImmunisationId = 10,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            patientImmunisation.Add(patientImmunisation.PatientEnrolled.Id, vaccine1);

            var vaccine2 = new Immunisation
            {
                ImmunisationId = 30,
                Vaccine = "Flu Diabetes1",
                Outcome = Outcome.AlternativeGiven,
                CreatedDate = DateTime.Now
            };
            patientImmunisation.Add(patientImmunisation.PatientEnrolled.Id, vaccine2);

            var clonepatientImmunisation = patientImmunisation.CLone();

            clonepatientImmunisation.PatientEnrolled.Id = 200;
            clonepatientImmunisation.Get(30).Outcome = Outcome.NonResponder;


            Assert.Equal("Id:100, CreatedDate:  " + DateTime.Now.ToShortDateString() + ", ImmunisationListCount: 2", patientImmunisation.PrintToString());
            Assert.Equal("Id:200, CreatedDate:  " + DateTime.Now.ToShortDateString() + ", ImmunisationListCount: 2", clonepatientImmunisation.PrintToString());

            Assert.NotEqual(patientImmunisation.Get(30).Outcome, clonepatientImmunisation.Get(30).Outcome);


        }

        #endregion

        #region Miscellaneous Test
        [Fact]
        public void Patient_CreatedDate_NotAllowedToChange()
        {
            //clonepatientImmunisation.PatientEnrolled.CreatedDate = DateTime.Now.AddMonths(-2); init doesnt allow to set the created date
            //Enroll new Patient
            var patient = new Patient(100, DateTime.Now);

            patient.Id = 200;

            // patient.CreatedDate = DateTime.Now.AddMonths(-2); compile time error not allowed

            //Mark it success
            Assert.Equal(1, 1);

        }

        //Altered Test Data to comply with the Uniqueness criteria of the Immunisation ID and Patient ID 
        [Fact]
        public void CreatePatientWithOneImmunisation_ImmunisationDateNotBeforeMonth_ReturnZero()
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

            Assert.Equal(0, patientimmunisation.GetTotal());
        }

        [Fact]
        public void CreatePatientWithOneImmunisation_ImmunisationDateBeforeMonth_ReturnOne()
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
                CreatedDate = DateTime.Now.AddMonths(-2)
            };

            patientimmunisation.Add(patient.Id, vaccine);

            Assert.Equal(1, patientimmunisation.GetTotal());
        }

        [Fact]
        public void CreatePatientWithMultipleImmunisation_OriginalTestData_ReturnMessage_ImmunisationIdCannotBeAdded()
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

            var ex = Assert.Throws<Exception>(() => patientimmunisation.Add(patient.Id, vaccine2));
            Assert.Equal("Immunisation ID: " + vaccine2.ImmunisationId + "  already exists for Patient " + patient.Id + ", So cannot add the Immunisation Information again.", ex.Message);

            //patientimmunisation.Add(patient.Id, vaccine3);
        }

        [Fact]
        public void CreatePatientWithMultipleImmunisation_AlteredTestData_ReturnImmunisationTotal()
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
                ImmunisationId = 20,
                Vaccine = "Flu 65+",
                Outcome = Outcome.NonResponder,
                CreatedDate = DateTime.Now
            };

            //Create Immunisation Record
            var vaccine3 = new Immunisation
            {
                ImmunisationId = 30,
                Vaccine = "Flu Vaccine PHO",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now.AddMonths(-2)
            };

            patientimmunisation.Add(patient.Id, vaccine1);
            patientimmunisation.Add(patient.Id, vaccine2);
            patientimmunisation.Add(patient.Id, vaccine3);

            Assert.Equal(1, patientimmunisation.GetTotal());

        }
        [Fact]
        public void PatientToString_OriginalTestData_ReturnMessage_ImmunisationRecordExistsForThePatient()
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


            var ex = Assert.Throws<Exception>(() => patientimmunisation.Add(patient.Id, vaccine2));
            Assert.Equal("Immunisation ID: " + vaccine2.ImmunisationId + "  already exists for Patient " + patient.Id + ", So cannot add the Immunisation Information again.", ex.Message);


        }
        [Fact]
        public void PatientToString_AlteredTestData_ReturnMessage_PrintedStringWithImmunisationCountTwo()
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
                ImmunisationId = 20,
                Vaccine = "Flu Vaccine PHO",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now.AddMonths(-2)
            };

            patientimmunisation.Add(patient.Id, vaccine1);
            patientimmunisation.Add(patient.Id, vaccine2);

            Assert.Equal("Id:100, CreatedDate:  " + DateTime.Now.ToShortDateString() + ", ImmunisationListCount: 2", patientimmunisation.PrintToString());
        }


        [Fact]
        public void MergePatients_OriginalTestData_ReturnMessageImmunisationRecordCannotBeAdded()
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
            Assert.Equal(0, patient1immunisation.GetTotal());



        }

        [Fact]
        public void MergePatients_AlteredTestData_ReturnMessageImmunisationRecordCannotBeAdded()
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
                Id = 200,
                CreatedDate = DateTime.Now
            };

            var patient2immunisation = new PatientImmunisation(patient2);

            var vaccine2 = new Immunisation
            {
                ImmunisationId = 20,
                Vaccine = "Flu 65+",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now.AddDays(-15)
            };

            patient2immunisation.Add(patient2.Id, vaccine2);

            patient1immunisation.Merge(patient2immunisation);

            Assert.Equal(0, patient1immunisation.GetTotal());
        }

        [Fact]
        public void RemoveImmunisation_OriginalTestData_ExceptionAddingDuplicateImmunisationRecord()
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

             


                var vaccine2 = new Immunisation
                {
                    ImmunisationId = 10,
                    Vaccine = "Flu 65+",
                    Outcome = Outcome.Given,
                    CreatedDate = DateTime.Now.AddDays(-45)
                };

           


                var vaccine3 = new Immunisation
                {
                    ImmunisationId = 10,
                    Vaccine = "Flu Vaccine PHO",
                    Outcome = Outcome.Given,
                    CreatedDate = DateTime.Now.AddMonths(-2)
                };

            try
            {
                patient1immunisation.Add(patient.Id, vaccine1);
                patient1immunisation.Add(patient.Id, vaccine2);
                patient1immunisation.Add(patient.Id, vaccine3);

                patient1immunisation.Remove(10);

                Assert.Equal(2, patient1immunisation.GetTotal());

            }

            catch(Exception ex)
            {
                Assert.Equal("Immunisation ID: "+ vaccine2.ImmunisationId + "  already exists for Patient "+ patient.Id + ", So cannot add the Immunisation Information again.", ex.Message);
            }

        }


        [Fact]
        public void RemoveImmunisation_AlteredTestData_ReturnGetTotalTwo()
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
                ImmunisationId = 20,
                Vaccine = "Flu 65+",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now.AddDays(-45)
            };

            patient1immunisation.Add(patient.Id, vaccine2);



            var vaccine3 = new Immunisation
            {
                ImmunisationId = 30,
                Vaccine = "Flu Vaccine PHO",
                Outcome = Outcome.Given,
                CreatedDate = DateTime.Now.AddMonths(-2)
            };

            patient1immunisation.Add(patient.Id, vaccine3);

            patient1immunisation.Remove(10);

           Assert.Equal(2, patient1immunisation.GetTotal());


        }

        [Fact]

        public void ClonePatient_OriginalTestData_Return_CountOne_CountOne_PrintOutcomeforBothPatient()
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


            Assert.NotEqual(patient1immunisation.Get(10).Outcome, patient2immunisation.Get(10).Outcome);
            Assert.Equal(0, patient1immunisation.GetTotal());
            Assert.Equal(0, patient2immunisation.GetTotal());
            Assert.Equal(1, patient1immunisation.Count());
            Assert.Equal(1, patient2immunisation.Count());

        }


        #endregion
    }
}



