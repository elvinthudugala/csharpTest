using MohioTechnicalBaseTest.Business.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MohioTechnicalBaseTest.Business.ModelExtension
{
    public static class PatientImmunisationExtension
    {
        /// <summary>
        /// Add Immmunisation Record to the PatientImmunisation Entry
        /// WARNING: Immunisation ID Must be unique for Patient , so if we try adding multiple Immunisation record with Same ID, they will be ignored
        /// </summary>
        /// <param name="value"></param>
        /// <param name="enroller"></param>
        /// <param name="vaccines"></param>
        public static void Add(this PatientImmunisation value, int enroller, Immunisation vaccines)
        {
            try
            {
                if (value == null) throw new Exception("PatientImmunisation Record is not set");

                if (value.PatientEnrolled.Id == enroller)
                {
                    if (value.ImmunisationList == null) value.ImmunisationList = new List<Immunisation>();

                    //check if the immunisation record already exist if so then dont add it 
                    if (vaccines != null)
                    {
                        var immunisationPresent = value.ImmunisationList.Where(d => d.ImmunisationId == vaccines.ImmunisationId).ToList();

                        if (immunisationPresent.Count() == 0)
                            value.ImmunisationList.Add(vaccines);
                        else
                            throw new Exception("Immunisation ID: " + vaccines.ImmunisationId + "  already exists for Patient " + value.PatientEnrolled.Id + ", So cannot add the Immunisation Information again.");

                    }
                    else
                        throw new Exception("No Immunisation information passed");


                }
                else
                    throw new Exception("PatientImmunisation Record not match with passed Patient Id");
            }
            catch (Exception ex)
            {
                throw;

            }

        }

        /// <summary>
        /// The total count the Immunisation Given 1 month before
        /// </summary>
        public static decimal GetTotal(this PatientImmunisation value)
        {
            try
            {

                if (value == null) return 0;

                if (value.ImmunisationList == null) return 0;

                var immunisationListOneMonthBefore = value.ImmunisationList.Where(d => d.CreatedDate < DateTime.Now.AddMonths(-1));

                return immunisationListOneMonthBefore.Count();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// The total count of the Immunisation Given 
        /// </summary>
        public static decimal Count(this PatientImmunisation value)
        {
            try
            {

                if (value == null) return 0;

                if (value.ImmunisationList == null) return 0;

                return value.ImmunisationList.Count();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Print the Patient Immunisation Details
        /// </summary>
        public static string PrintToString(this PatientImmunisation value)
        {
            if (value == null) return $"Patient Not Available";
            return $"Id:{value.PatientEnrolled.Id}, CreatedDate:  {value.PatientEnrolled.CreatedDate.ToShortDateString()}, ImmunisationListCount: {value.Count()}";
        }





        /// <summary>
        /// Appends the ImmunisationList from the source to the current patient, immunisationId must be unique
        /// </summary>
        /// <param name="sourcePatient">patient to merge from</param>
        public static void Merge(this PatientImmunisation value, PatientImmunisation sourcePatient)
        {
            try
            {

                if (sourcePatient == null) return;

                if (value == null) { value = sourcePatient; return; };

                if (sourcePatient.ImmunisationList == null) return;

                if (sourcePatient.ImmunisationList.Count == 0) return;


                //check if immunisation record exist in the destination , if not then only merge it from sourcePatient
                foreach (Immunisation immunisation in sourcePatient.ImmunisationList)
                {
                    var immunisationPresent = value.ImmunisationList.Where(d => d.ImmunisationId == immunisation.ImmunisationId);

                    if (immunisationPresent.Count() == 0)
                        value.ImmunisationList.Add(immunisation);

                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        /// <summary>
        /// Return the Immunisation Record for the Immunisation ID passed
        /// </summary>
        /// <param name="value"></param>
        /// <param name="immunisationId"></param>
        /// <returns></returns>
        public static Immunisation Get(this PatientImmunisation value, int immunisationId)
        {
            try
            {

                if (value == null) return null;

                if (value.ImmunisationList == null) return null;

                var immunisation = value.ImmunisationList.Where(d => d.ImmunisationId == immunisationId).ToList();

                if (immunisation.Count() == 1)
                    return immunisation[0];

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Remove the Immunisation Record for the Patient 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="immunisationId"></param>
        public static void Remove(this PatientImmunisation value, int immunisationId)
        {
            try
            {

                if (value == null) return;

                if (value.ImmunisationList == null) return;

                var immunisationListWithout = value.ImmunisationList.Where(d => d.ImmunisationId != immunisationId).ToList();

                value.ImmunisationList = immunisationListWithout;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Creates a deep clone of the current Patient (all fields and properties)
        /// Serialize the original object using NewtonSoft into JSON and then return the deserialises the object which returns new object, new reference
        /// </summary>
        public static T CLone<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

    }
}
