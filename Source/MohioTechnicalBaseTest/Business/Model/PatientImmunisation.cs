using System;
using System.Collections.Generic;

namespace MohioTechnicalBaseTest.Business.Model
{

    public class PatientImmunisation
    {
        public PatientImmunisation(Patient patientEnrolled)
        {
            PatientEnrolled = patientEnrolled ?? throw new ArgumentNullException(nameof(patientEnrolled));
        }

        public Patient PatientEnrolled { get; set; }

        public List<Immunisation> ImmunisationList { get; set; }
    }
}
