using Grpc.Net.Client;
using Microsoft.WindowsAzure.Storage;
using MyAzureBlob;
using MyDependencies;
using MyPostgreSQL;
using ProtoBuf.Grpc.Client;
using Swisschain.PersonalData.Postgres;

namespace Swisschain.PersonalData.Server
{
    public static class ServicesBinder
    {
        public static void BindPostgres(this IServiceRegistrator sr, SettingsModel settingsModel, string key)
        {
            sr.Register(new PersonalDataPostgresRepository(
                new PostgresConnection(settingsModel.PostgresConnString)));
            
            sr.Register(new TraderDocumentsPostgresRepository(new PostgresConnection(settingsModel.PostgresConnString)));
        }

        public static void BindBlobService(this IServiceRegistrator sr, SettingsModel settingsModel)
        {
            var storageAccount = CloudStorageAccount.Parse(settingsModel.AzureStoragePdConnString);

            sr.Register<IAzureBlobContainer>(
                new MyAzureBlobContainer(storageAccount, settingsModel.AzureKycBlobContainerName)
            );
        }

        /*
        public static void BindGrpcServices(this IServiceRegistrator sr, SettingsModel settings)
        {
            GrpcClientFactory.AllowUnencryptedHttp2 = true;

            sr.Register(GrpcChannel
                .ForAddress(settings.AuditLogServiceUrl)
                .CreateGrpcService<IAuditLogServiceGrpc>());
        }
        */

    }
}