using System.Runtime.Serialization;

namespace Swisschain.PersonalData.Grpc.Models
{
    [DataContract]
    public class ResultGrpcResponse
    {
        [DataMember(Order = 1)]
        public bool Ok { get; set; }
    }
}