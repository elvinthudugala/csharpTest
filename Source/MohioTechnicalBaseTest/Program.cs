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

/*   Assumption: - 
 *   1. Original Test data provided in the below method is not changed, so console output contains result + exception:
 *                              CreatePatientWithOneImmunisation, 
 *                              CreatePatientWithMultipleImmunisation,
 *                              RemoveImmunisation
 *                              MergePatients
 *                              ClonePatient
 *                              PatientToString
 * 
 *   2. Test Cases covered the duplicate Immunisation Id and Patient Id 
 *   
 *   3. Few Use Cases followed are: 
 *              a. If the Patient contains Immunisation Records, and if someone tries to Add or Merge the Immunisation Record with same ID then it does not add or merge, just ignore
 *              b. Patient Created Date can be set once while creation but cannot be changed. Instead of private setter, I have used .net5 init feature
 *              
 *   4. Application Architecture is below 
 *         
 *              UI           =>          Application       =>          Business     => (future scope =>  Infrastructure   Database )
  *         
 *       Console Application             
 *         Program.cs                PatientImmunisationAPI           Model  ( Just Entity Classes)
 *                                                                    ModelExtension (Behavior of Entity Classes)
 *   5. Added XUnit Test Project
 *   
 *   6. Added NewtonsoftJSON Project for Serialisation and Deserialisation
 * */

using System;
using MohioTechnicalBaseTest.Application;

namespace MohioTechnicalBaseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Mohio Technical Test 1");
            PatientImmunisationAPI.CreatePatientWithOneImmunisation();
            PatientImmunisationAPI.CreatePatientWithMultipleImmunisation();
            PatientImmunisationAPI.RemoveImmunisation();
            PatientImmunisationAPI.MergePatients();
            PatientImmunisationAPI.ClonePatient();
            PatientImmunisationAPI.PatientToString();
        }


    }
}
