using System;
using Swisschain.Domains.PersonalData;
using Swisschain.PersonalData.Grpc.Models;


namespace Swisschain.PersonalData.Postgres
{
    public class PersonalData 
    {
        public string Id { get; set; }
        
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string City { get; set; }
        
        public string Phone { get; set; }
        
        public PersonalDataSex Sex { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        public string PostalCode { get; set; }
        
        public string CountryOfCitizenship { get; set; }
        
        public string CountryOfResidence { get; set; }
        
        public PersonalDataKycStatus Kyc { get; set; }
        
        public DateTime? Confirm { get; set; }
        
        public string Address { get; set; }
        
        public string IpOfRegistration { get; set; }
        
        public string CountryOfRegistration { get; set; }
        
        public bool IsInternal { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public string EmailHash { get; set; }
    }
}