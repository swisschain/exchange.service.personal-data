using System;
using System.Runtime.Serialization;
using Swisschain.Domains.PersonalData;

namespace Swisschain.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class UpdatePersonalDataGrpcContract
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }
        
        [DataMember(Order = 2)]
        public string FirstName { get; set; }
        
        [DataMember(Order = 3)]
        public string LastName { get; set; }
        
        [DataMember(Order = 4)]
        public PersonalDataSex Sex { get; set; }
        
        [DataMember(Order = 5)]
        public DateTime? DateOfBirth { get; set; }
        
        [DataMember(Order = 6)]
        public string CountryOfResidence { get; set; }
        
        [DataMember(Order = 7)]
        public string CountryOfCitizenship { get; set; }
        
        [DataMember(Order = 8)]
        public string City { get; set; }
        
        [DataMember(Order = 9)]
        public string PostalCode { get; set; }
        
        [DataMember(Order = 10)]
        public string Phone { get; set; }
        
        [DataMember(Order = 11)]
        public AuditLogGrpcContract AuditLog { get; set; }

        [DataMember(Order = 12)]
        public string Address { get; set; }

    }
}