using System.Linq;
using System.Threading.Tasks;
using Swisschain.Cryptography;
using Swisschain.PersonalData.Grpc;
using Swisschain.PersonalData.Grpc.Contracts;
using Swisschain.PersonalData.Grpc.Documents;
using Swisschain.PersonalData.Server.Mappers;

namespace Swisschain.PersonalData.Server.Grpc
{
    public class DocumentsServiceGrpc : IDocumentsServiceGrpc
    {
        public async ValueTask<TraderDocumentGrpcModel> SaveDocumentAsync(UploadDocumentGrpcContract request)
        {

            var model = request.ToDomain();

            await ServiceLocator.TraderDocumentsPostgresRepository.Add(model);

            var encodedFileContent = AesEncodeDecode.Encode(request.Data, ServiceLocator.EncodingKey);

            await ServiceLocator.AzureBlobContainer.UploadToBlobAsync(model.Id, encodedFileContent);

            // await ServiceLocator.AuditLogGrpcService.DispatchAuditLogsAction(request.TraderId, "kyc",
            //     model.Id,
            //     $"Trader uploaded a document with id: {model.Id}");

            return model.ToGrpc();

        }

        public async ValueTask<GetDocumentContentGrpcResponse> DownloadDocumentAsync(DocumentGrpcContract request)
        {

            var document = await ServiceLocator.TraderDocumentsPostgresRepository
                .GetDocumentsAsync(request.TraderId, request.Id);

            if (document == null)
                return
                    new GetDocumentContentGrpcResponse
                    {
                        DocumentContent = null
                    };

            var fileContent = await ServiceLocator.AzureBlobContainer.DownloadBlobAsync(document.FileName);
            var encodedFileContent = AesEncodeDecode.Decode(fileContent.ToArray(), ServiceLocator.EncodingKey);

            return new GetDocumentContentGrpcResponse
            {
                DocumentContent = new TraderDocumentContentGrpcModel
                {
                    Mime = document.Mime,
                    Data = encodedFileContent,
                    FileName = document.FileName
                }
            };

        }

        public async ValueTask DeleteDocumentAsync(DocumentGrpcContract request)
        {
            await ServiceLocator.TraderDocumentsPostgresRepository.DeleteAsync(request.TraderId, request.Id);
        }

        public async ValueTask<GetDocumentsByUserResponse> DocumentsByUserAsync(GetDocumentsByUserContract request)
        {

            var documents = await ServiceLocator.TraderDocumentsPostgresRepository
                .GetDocumentsAsync(request.TraderId);

            var result = documents.Select(x => x.ToGrpc());

            return new GetDocumentsByUserResponse
            {
                Documents = result.ToArray()
            };

        }
    }
}