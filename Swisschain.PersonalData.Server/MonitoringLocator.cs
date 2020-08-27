using Prometheus;

namespace Swisschain.PersonalData.Server
{
    public class MonitoringLocator
    {
        public static Summary RequestDurationSummary = GetRequestDurationSummary();
        
        private static Summary GetRequestDurationSummary()
        {
            var config = new SummaryConfiguration
            {
                LabelNames = new[] {"method"}
            };

            return Metrics.CreateSummary("personal_data_request_duration", "Average request duration", config);
        }
    }
}