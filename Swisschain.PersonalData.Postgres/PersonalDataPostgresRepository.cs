using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPostgreSQL;
using Swisschain.PersonalData.Grpc.Models;

namespace Swisschain.PersonalData.Postgres
{
    public class PersonalDataPostgresRepository
    {
        private readonly IPostgresConnection _postgresConnection;

        private const string TableName = "personaldata";
        
        public PersonalDataPostgresRepository(IPostgresConnection postgresConnection)
        {
            _postgresConnection = postgresConnection ?? throw new ArgumentNullException(nameof(postgresConnection));
        }

        private async Task<PersonalDataPostgresEntity> GetEntityAsync(string id, byte[] initKey)
        {
            var sql = @$"SELECT * FROM {TableName} where id = @id";

            var result = await _postgresConnection
                .GetFirstRecordOrNullAsync<PersonalDataPostgresEntity>(sql, new {id});

            result?.Decode(initKey);

            return result;
        }

        public async Task CreateAsync(PersonalDataPostgresEntity pd, byte[] initKey)
        {
                pd.Encode(initKey);

                await _postgresConnection.Insert(TableName).SetInsertModel(pd).ExecuteAsync();
        }

        public async ValueTask<PersonalDataPostgresEntity> GetByIdAsync(string id, byte[] initKey)
        {
            return await GetEntityAsync(id, initKey);
        }

        public async ValueTask<PersonalDataPostgresEntity> GetByEmailAsync(string email)
        {

            var sql = @$"SELECT * FROM {TableName} where emailhash = @Email";

            return await _postgresConnection.GetFirstRecordOrNullAsync<PersonalDataPostgresEntity>(sql, new
            {
                Email = email.EncodeToSha1()
            });

        }

        public async Task ConfirmAsync(string id, DateTime confirm, byte[] initKey)
        {

            var entity = await GetEntityAsync(id, initKey);

            if (entity == null)
                throw new Exception($"Entity not found with id: {id}");

            entity.Confirm = confirm;

            entity.Encode(initKey);

            await _postgresConnection.Update(TableName)
                .SetUpdateModel(entity)
                .SetWhereCondition("id = @id", new {id = entity.Id})
                .ExecuteAsync();
        }

        public async Task UpdateKycAsync(string id, PersonalDataKycStatus kyc, byte[] initKey)
        {
            var entity = await GetEntityAsync(id, initKey);

            if (entity == null)
                throw new Exception($"Entity not found with id: {id}");

            entity.KYC = (int) kyc;

            entity.Encode(initKey);

            await _postgresConnection.Update(TableName)
                .SetUpdateModel(entity)
                .SetWhereCondition("id = @id", new {id = entity.Id})
                .ExecuteAsync();

        }

        public async Task UpdateAsync(PersonalDataPostgresUpdateEntity update, byte[] initKey)
        {

                var entity = await GetEntityAsync(update.Id, initKey);
                
                if (entity == null)
                    throw new Exception($"Entity not found with id: {update.Id}");

                if (update.FirstName != null)
                    entity.FirstName = update.FirstName;
            
                if (update.LastName != null)
                    entity.LastName = update.LastName;
            
                if (update.City != null)
                    entity.City = update.City;

                if (update.Phone != null)
                    entity.Phone = update.Phone;

                if (update.PostalCode != null)
                    entity.PostalCode = update.PostalCode;

                if (update.CountryOfCitizenship != null)
                    entity.CountryOfCitizenship = update.CountryOfCitizenship;

                if (update.CountryOfResidence != null)
                    entity.CountryOfResidence = update.CountryOfResidence;

                if (update.Sex != null)
                    entity.Sex = (int) update.Sex;
            
                if (update.BirthDay != null)
                    entity.DateOfBirth = update.BirthDay;

                if (update.Address != null)
                    entity.Address = update.Address;

                entity.Encode(initKey);

                await _postgresConnection.Update(TableName)
                    .SetUpdateModel(entity)
                    .SetWhereCondition("id = @id", new { id = entity.Id})
                    .ExecuteAsync();
        }

        public async Task<IEnumerable<PersonalDataPostgresEntity>> GetByKycStatus(PersonalDataKycStatus kycStatus,
            byte[] initKey)
        {

            var sql = @$"SELECT * FROM {TableName} where kyc = @kysStatus";

            if (kycStatus == PersonalDataKycStatus.NotVerified)
                sql += " OR kyc IS NULL";

            var entities =
                (await _postgresConnection.GetRecordsAsync<PersonalDataPostgresEntity>(sql)).ToList();

            return entities.Select(item =>
            {
                item.Decode(initKey);
                return item;
            });
        }
    }
}