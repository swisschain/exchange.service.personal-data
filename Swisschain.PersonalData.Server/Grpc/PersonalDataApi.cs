using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Swisschain.PersonalData.Grpc;
using Swisschain.PersonalData.Grpc.Contracts;
using Swisschain.PersonalData.Grpc.Models;
using Swisschain.PersonalData.Postgres;
using Swisschain.PersonalData.Server.Mappers;

namespace Swisschain.PersonalData.Server.Grpc
{
    public class PersonalDataApi : IPersonalDataServiceGrpc
    {
        private static string ToJson(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public async ValueTask<ResultGrpcResponse> RegisterAsync(RegisterPersonalDataGrpcModel request)
        {

            var isInternal = Regex.IsMatch(request.Email, ServiceLocator.SettingsModel.InternalAccountPatterns);

            var pd = request.ToDomainModel(isInternal);

            await ServiceLocator.PersonalDataRepository.CreateAsync(pd, ServiceLocator.EncodingKey);

            var logData = ToJson(pd);

            /*
            var log = new AuditLogGrpcModel
            {
                TraderId = request.Id,
                ProcessId = request.AuditLog.ProcessId,
                Ip = request.AuditLog.Ip,
                ServiceName = request.AuditLog.ServiceName,
                Context = request.AuditLog.Context,
                Before = string.Empty,
                UpdatedData = logData,
                After = logData
            };

            await ServiceLocator.AuditLogServiceGrpc.RegisterEventAsync(log);
            */

            return new ResultGrpcResponse {Ok = true};

        }

        public async ValueTask<ResultGrpcResponse> UpdateAsync(UpdatePersonalDataGrpcContract request)
        {

            var pd = await ServiceLocator.PersonalDataRepository.GetByIdAsync(request.Id, ServiceLocator.EncodingKey);

            await ServiceLocator.PersonalDataRepository.UpdateAsync(new PersonalDataPostgresUpdateEntity
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                City = request.City,
                Phone = request.Phone,
                PostalCode = request.PostalCode,
                CountryOfCitizenship = request.CountryOfCitizenship,
                CountryOfResidence = request.CountryOfResidence,
                Address = request.Address,
                Sex = request.Sex,
                BirthDay = request.DateOfBirth
            }, ServiceLocator.EncodingKey);

            var updatedPd =
                await ServiceLocator.PersonalDataRepository.GetByIdAsync(request.Id, ServiceLocator.EncodingKey);

            /*
            var log = new AuditLogGrpcModel
            {
                TraderId = request.Id,
                ProcessId = request.AuditLog.ProcessId,
                Ip = request.AuditLog.Ip,
                ServiceName = request.AuditLog.ServiceName,
                Context = request.AuditLog.Context,
                Before = ToJson(pd),
                UpdatedData = ToJson(request),
                After = ToJson(updatedPd)
            };

            await ServiceLocator.AuditLogServiceGrpc.RegisterEventAsync(log);
            */

            return new ResultGrpcResponse {Ok = true};

        }


        public async ValueTask ConfirmAsync(ConfirmGrpcModel request)
        {

            var pd = await ServiceLocator.PersonalDataRepository.GetByIdAsync(request.Id, ServiceLocator.EncodingKey);

            await ServiceLocator.PersonalDataRepository.ConfirmAsync(request.Id, request.Confirm,
                ServiceLocator.EncodingKey);

            var updatedPd =
                await ServiceLocator.PersonalDataRepository.GetByIdAsync(request.Id, ServiceLocator.EncodingKey);

            /*
            var log = new AuditLogGrpcModel
            {
                TraderId = request.Id,
                ProcessId = string.Empty,
                Ip = request.AuditLog.Ip,
                ServiceName = request.AuditLog.ServiceName,
                Context = request.AuditLog.Context,
                Before = ToJson(pd),
                UpdatedData = ToJson(request),
                After = ToJson(updatedPd),
            };

            await ServiceLocator.AuditLogServiceGrpc.RegisterEventAsync(log);
            */

        }

        public async ValueTask UpdateKycAsync(UpdateKycGrpcContract request)
        {
            var pd = await ServiceLocator.PersonalDataRepository.GetByIdAsync(request.Id, ServiceLocator.EncodingKey);

            await ServiceLocator.PersonalDataRepository.UpdateKycAsync(request.Id, request.Kyc, ServiceLocator.EncodingKey);
            
            var updatedPd = await ServiceLocator.PersonalDataRepository.GetByIdAsync(request.Id, ServiceLocator.EncodingKey);
            
            /*
            var log = new AuditLogGrpcModel
            {
                TraderId = request.Id,
                ProcessId = request.AuditLog.ProcessId,
                Ip = request.AuditLog.Ip,
                ServiceName = request.AuditLog.ServiceName,
                Context = request.AuditLog.Context,
                Before = ToJson(pd),
                UpdatedData = ToJson(request.Kyc),
                After = ToJson(updatedPd),
            };
            
            await ServiceLocator.AuditLogServiceGrpc.RegisterEventAsync(log);
            */
        }

        public ValueTask<GetPersonalDataByStatusResponse> GetPersonalDataByStatus(GetPersonalDataByStatusRequest request)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<PersonalDataGrpcResponseContract> GetByIdAsync(string id)
        {
            Console.WriteLine("Requesting data by ID: "+id);
            
            var pd = await ServiceLocator.PersonalDataRepository.GetByIdAsync(id, ServiceLocator.EncodingKey);

            var response = new PersonalDataGrpcResponseContract();
            
            if (pd != null)
                response.PersonalData = pd.ToGrpcModel();

            return response;
        }

        public async ValueTask<PersonalDataGrpcResponseContract> GetByEmail(string email)
        {
            var pd = await ServiceLocator.PersonalDataRepository.GetByIdAsync(email, ServiceLocator.EncodingKey);
            
            var response = new PersonalDataGrpcResponseContract();
            
            if (pd != null)
                response.PersonalData = pd.ToGrpcModel();

            return response;
        }
    }
}