using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Prometheus;

namespace Swisschain.PersonalData.Server
{
    public class RequestDurationInterceptor : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            using (MonitoringLocator.RequestDurationSummary.WithLabels(context.Method).NewTimer())
            {
                return await continuation(request, context);
            }
        }
    }
}