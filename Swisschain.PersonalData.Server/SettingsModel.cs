

using MyYamlSettingsParser;

namespace Swisschain.PersonalData.Server
{
    public class SettingsModel
    {
        [YamlProperty("PersonalDataServer.Db.AzureStoragePdConnString")]
        public string AzureStoragePdConnString { get; set; }
        
        [YamlProperty("PersonalDataServer.Db.AzureStoragePdIndexConnString")]
        public string AzureStoragePdIndexConnString { get; set; }
        
        [YamlProperty("PersonalDataServer.AuditLogServiceUrl")]
        public string AuditLogServiceUrl { get; set; }
        
        [YamlProperty("PersonalDataServer.Db.PostgresConnString")]
        public string PostgresConnString { get; set; }
        
        [YamlProperty("PersonalDataServer.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }
        
        [YamlProperty("PersonalDataServer.InternalAccountPatterns")]
        public string InternalAccountPatterns { get; set; }
        
        [YamlProperty("PersonalDataServer.KycBlobContainerName")]
        public string AzureKycBlobContainerName { get; set; }
    }
}