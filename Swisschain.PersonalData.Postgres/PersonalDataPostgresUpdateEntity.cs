using System;
using Swisschain.Domains.PersonalData;

namespace Swisschain.PersonalData.Postgres
{
    public class PersonalDataPostgresUpdateEntity 
    {
        public string Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string City { get; set; }
        
        public string Phone { get; set; }
        
        public string PostalCode { get; set; }
        
        public string CountryOfCitizenship { get; set; }
        
        public string CountryOfResidence { get; set; }
        
        public string Address { get; set; }
        
        public PersonalDataSex? Sex { get; set; }
        
        public DateTime? BirthDay { get; set; }

        
    }
}