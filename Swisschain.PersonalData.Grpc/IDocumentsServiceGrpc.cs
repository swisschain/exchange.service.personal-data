using System.ServiceModel;
using System.Threading.Tasks;
using Swisschain.PersonalData.Grpc.Contracts;
using Swisschain.PersonalData.Grpc.Documents;

namespace Swisschain.PersonalData.Grpc
{
    [ServiceContract(Name = "DocumentsService")]
    public interface IDocumentsServiceGrpc
    {
        [OperationContract(Action = "SaveDocument")]
        ValueTask<TraderDocumentGrpcModel> SaveDocumentAsync(UploadDocumentGrpcContract request);

        [OperationContract(Action = "DownloadDocument")]
        ValueTask<GetDocumentContentGrpcResponse> DownloadDocumentAsync(DocumentGrpcContract request);
        
        [OperationContract(Action = "DeleteDocument")]
        ValueTask DeleteDocumentAsync(DocumentGrpcContract request);
        
        [OperationContract(Action = "DocumentsByUser")]
        ValueTask<GetDocumentsByUserResponse> DocumentsByUserAsync(GetDocumentsByUserContract request);
    }
}