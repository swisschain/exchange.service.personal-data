using System.Text;
using MyAzureBlob;
using MyDependencies;
using Swisschain.PersonalData.Postgres;

namespace Swisschain.PersonalData.Server
{
    public static class ServiceLocator
    {
        public static PersonalDataPostgresRepository PersonalDataRepository { get; private set; }
        
      //  public static IAuditLogServiceGrpc AuditLogServiceGrpc { get; private set; }
        
        public static IAzureBlobContainer AzureBlobContainer { get; private set; }
        
        public static SettingsModel SettingsModel { get; set; }
        
        public static TraderDocumentsPostgresRepository TraderDocumentsPostgresRepository { get; set; }
        
        public static byte[] EncodingKey { get; set; }
        
        public static void Init(IServiceResolver serviceResolver, string encodingKey)
        {
            TraderDocumentsPostgresRepository = serviceResolver.GetService<TraderDocumentsPostgresRepository>();
            PersonalDataRepository = serviceResolver.GetService<PersonalDataPostgresRepository>();
            AzureBlobContainer = serviceResolver.GetService<IAzureBlobContainer>();
          //  AuditLogServiceGrpc = serviceResolver.GetService<IAuditLogServiceGrpc>();
            
            EncodingKey = Encoding.UTF8.GetBytes(encodingKey);
        }
    }
}