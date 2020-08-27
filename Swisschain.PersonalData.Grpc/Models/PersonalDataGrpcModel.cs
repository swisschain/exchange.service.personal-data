using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Swisschain.Domains.PersonalData;
using Swisschain.PersonalData.Grpc.Contracts;

namespace Swisschain.PersonalData.Grpc.Models
{
    [DataContract]
    public class PersonalDataGrpcModel
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }
        
        [DataMember(Order = 2)]
        public string Email { get; set; }

        [DataMember(Order = 3)]
        public string FirstName { get; set; }
        
        [DataMember(Order = 4)]
        public string LastName { get; set; }
        
        [DataMember(Order = 5)]
        public PersonalDataSex Sex { get; set; }
        
        [DataMember(Order = 6)]
        public DateTime? DateOfBirth { get; set; }
        
        [DataMember(Order = 7)]
        public string CountryOfResidence { get; set; }
        
        [DataMember(Order = 8)]
        public string CountryOfCitizenship { get; set; }
        
        [DataMember(Order = 9)]
        public string City { get; set; }
        
        [DataMember(Order = 10)]
        public string PostalCode { get; set; }
        
        [DataMember(Order = 11)]
        public string Phone { get; set; }
        
        [DataMember(Order = 12)]
        public PersonalDataKycStatus Kyc { get; set; }
        
        [DataMember(Order = 13)]
        public DateTime? Confirm { get; set; }
        
        [DataMember(Order = 14)]
        public AuditLogGrpcContract AuditLog { get; set; }
        
        [DataMember(Order = 15)]
        public string Address { get; set; }
        
        [DataMember(Order = 16)]
        public string CountryOfRegistration { get; set; }
        
        [DataMember(Order = 17)]
        public string IpOfRegistration { get; set; }
        
        [DataMember(Order = 18)]
        public IEnumerable<ExternalDataGrpcModel> ExternalData { get; set; }
    }
}