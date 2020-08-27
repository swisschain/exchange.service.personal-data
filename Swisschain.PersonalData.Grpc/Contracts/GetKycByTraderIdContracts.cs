using System.Collections.Generic;
using System.Runtime.Serialization;
using Swisschain.Domains.PersonalData;
using Swisschain.PersonalData.Grpc.Models;

namespace Swisschain.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class GetPersonalDataByStatusRequest
    {
        [DataMember(Order = 1)]
        public PersonalDataKycStatus Status { get; set; }
    }
    
    [DataContract]
    public class GetPersonalDataByStatusResponse
    {
        [DataMember(Order = 1)]
        public IEnumerable<PersonalDataGrpcModel> PersonalDataModels { get; set; }
    }
}