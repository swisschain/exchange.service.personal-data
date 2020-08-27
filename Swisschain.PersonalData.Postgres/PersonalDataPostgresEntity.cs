using System;
using Newtonsoft.Json;
using Swisschain.Domains.PersonalData;
using Swisschain.PersonalData.Grpc.Models;

namespace Swisschain.PersonalData.Postgres
{
    public class PersonalDataPostgresEntity 
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        
        [JsonProperty("city")]
        public string City { get; set; }
        
        [JsonProperty("phone")]
        public string Phone { get; set; }
        
        [JsonProperty("dateofbirth")]
        public DateTime? DateOfBirth { get; set; }
        
        [JsonProperty("postalcode")]
        public string PostalCode { get; set; }
        
        [JsonProperty("countryofcitizenship")]
        public string CountryOfCitizenship { get; set; }
        
        [JsonProperty("countryofresidence")]
        public string CountryOfResidence { get; set; }
        
        [JsonProperty("emailhash")]
        public string EmailHash { get; set; }


        public PersonalDataSex GetPersonalDataSex()
        {
            if (Sex == null)
                return PersonalDataSex.Unknown;
            
            return (PersonalDataSex) Sex; 
        }

        [JsonProperty("sex")]
        public int? Sex { get; set; }

        public static int SexToDto(PersonalDataSex sex)
        {
            return (int) sex;
        }


        public PersonalDataKycStatus GetKycStatus()
        {
            if (KYC == null)
                return PersonalDataKycStatus.NotVerified;
                
            return (PersonalDataKycStatus) KYC;
        }


        [JsonProperty("kyc")]
        public int? KYC { get; set; }

        
        [JsonProperty("confirm")]
        public DateTime? Confirm { get; set; }
        
        [JsonProperty("address")]
        public string Address { get; set; }
        
        
        [JsonProperty("ipofregistration")]
        public string IpOfRegistration { get; set; }
        
        [JsonProperty("countryofregistration")]
        public string CountryOfRegistration { get; set; }
        
        [JsonProperty("isinternal")]
        public bool IsInternal { get; set; }

        [JsonProperty("createdat")]
        public DateTime CreatedAt { get; set; }

        
    }
}