using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyDependencies;
using MySettingsReader;
using Prometheus;
using ProtoBuf.Grpc.Server;
using Swisschain.PersonalData.Postgres;
using Swisschain.PersonalData.Server.Grpc;

namespace Swisschain.PersonalData.Server
{
    public class Startup
    {
        private const string EncodingKey = "ENCODING_KEY";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        
        public readonly MyIoc Ioc = new MyIoc();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            
            services.AddControllers();
            
            var settings = SettingsReader.GetSettings<SettingsModel>(".swisschain-personaldata");

            services.AddCodeFirstGrpc(options =>
            {
                options.MaxReceiveMessageSize = 5 * 1024 * 1024;
                options.MaxSendMessageSize = 5 * 1024 * 1024;
            });

            var key = GetEncodingKey();
            
            Console.WriteLine($"ENCODING_KEY is {key}");
            
            Ioc.BindBlobService(settings);

         //   Ioc.BindGrpcServices(settings);

            Ioc.BindPostgres(settings, key);
            
            ServiceLocator.Init(Ioc, key);

            ServiceLocator.SettingsModel = settings;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();



            app.UseRouting();

            app.UseEndpoints(

                endpoints =>
                {
                    endpoints.MapControllers();
                    
                    endpoints.MapGrpcService<PersonalDataApi>();
                    endpoints.MapGrpcService<DocumentsServiceGrpc>();
                    
                    endpoints.MapMetrics();
                });
        }
        
        private static IDictionary<string, string> GetEnvVariables()
        {
            var encodingKey = GetEncodingKey();
            
            return new Dictionary<string, string>
            {
                {EncodingKey, encodingKey.EncodeToSha1()}
            };
        }

        public static string GetEncodingKey()
        {
            var key = Environment.GetEnvironmentVariable(EncodingKey);
            
            if (string.IsNullOrEmpty(key))
                throw new Exception($"Env Variable {EncodingKey} is not found");

            Console.WriteLine($"Initialized EncodingKey length {key.Length}");
            Console.WriteLine($"start {key[0]}");
            Console.WriteLine($"end {key[^1]}");
            
            return key;
        }
    }
}