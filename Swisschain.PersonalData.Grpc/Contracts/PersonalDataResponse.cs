using System.Runtime.Serialization;
using Swisschain.PersonalData.Grpc.Models;

namespace Swisschain.PersonalData.Grpc.Contracts
{
    [DataContract]
    public class PersonalDataGrpcResponseContract
    {
        [DataMember(Order = 1)]
        public PersonalDataGrpcModel PersonalData { get; set; }
    }
}