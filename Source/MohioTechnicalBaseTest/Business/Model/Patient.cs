using Newtonsoft.Json;
using System;

namespace MohioTechnicalBaseTest
{

    public class Patient
    {


        /// <summary>
        /// Must be unique
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Must not be allowed to change.
        /// </summary>
        /// [JsonProperty]    Required for Cloning otherwise Serialisation Deserialisation will give the value as Null for datetime i..e 01/01/0001
        //[JsonProperty]   -- Required if use private setter . Not required with .NET 5 init feature
        public DateTime CreatedDate { get; init; }


        public Patient(int id)
        {
            Id = id;
            CreatedDate = DateTime.Now;
        }

        public Patient(int id, DateTime createdDate)
        {
            Id = id;
            CreatedDate = createdDate;
        }

        public Patient()
        {

        }

    }
}
