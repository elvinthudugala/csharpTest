using System;
using MohioTechnicalBaseTest.Common;
namespace MohioTechnicalBaseTest
{

    public class Immunisation
    {
        /// <summary>
        /// Must be unique for Patient
        /// </summary>
        public int ImmunisationId { get; set; }

        public string Vaccine { get; set; }

        public Outcome? Outcome { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
