using System.ServiceModel;
using System.Threading.Tasks;
using Swisschain.PersonalData.Grpc.Contracts;
using Swisschain.PersonalData.Grpc.Models;

namespace Swisschain.PersonalData.Grpc
{
    [ServiceContract(Name = "PersonalDataService")]
    public interface IPersonalDataServiceGrpc
    {
        [OperationContract(Action = "Register")]
        ValueTask<ResultGrpcResponse> RegisterAsync(RegisterPersonalDataGrpcModel personalData);
        
        [OperationContract(Action = "Update")]
        ValueTask<ResultGrpcResponse> UpdateAsync(UpdatePersonalDataGrpcContract personalData);
        
        [OperationContract(Action = "GetById")]
        ValueTask<PersonalDataGrpcResponseContract> GetByIdAsync(string id);
        
        [OperationContract(Action = "GetByEmail")]
        ValueTask<PersonalDataGrpcResponseContract> GetByEmail(string email);

        [OperationContract(Action = "Confirm")]
        ValueTask ConfirmAsync(ConfirmGrpcModel confirmData);
        
        [OperationContract(Action = "UpdateKyc")]
        ValueTask UpdateKycAsync(UpdateKycGrpcContract request);
        
        [OperationContract(Action = "GetPersonalDataByStatus")]
        ValueTask<GetPersonalDataByStatusResponse> GetPersonalDataByStatus(GetPersonalDataByStatusRequest request);
    }
}