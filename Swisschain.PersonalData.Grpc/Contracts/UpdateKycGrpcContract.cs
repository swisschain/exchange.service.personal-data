using System.Runtime.Serialization;
using Swisschain.Domains.PersonalData;
using Swisschain.PersonalData.Grpc.Models;

namespace Swisschain.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class UpdateKycGrpcContract
    {
        [DataMember(Order = 1)]
        public string Id { get; set; }
        
        [DataMember(Order = 2)]
        public PersonalDataKycStatus Kyc { get; set; }
        
        [DataMember(Order = 3)]
        public AuditLogGrpcContract AuditLog { get; set; }
    }
}