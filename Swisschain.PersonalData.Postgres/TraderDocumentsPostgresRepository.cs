using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPostgreSQL;

namespace Swisschain.PersonalData.Postgres
{
    public class TraderDocumentsPostgresRepository
    {
        private readonly IPostgresConnection _postgresConnection;

        public const string TableName = "traderdocuments";

        public TraderDocumentsPostgresRepository(IPostgresConnection postgresConnection)
        {
            _postgresConnection = postgresConnection ?? throw new ArgumentNullException(nameof(postgresConnection));
        }

        public async Task<IEnumerable<TraderDocument>> GetDocumentsAsync(string traderId)
        {
            var sql = $"select * from {TableName} where traderid = @TraderId and isdeleted = false";

            return await _postgresConnection.GetRecordsAsync<TraderDocument>(sql, new
            {
                TraderId = traderId,
            });

        }

        public async Task<TraderDocument> GetDocumentsAsync(string traderId, string documentId)
        {

            var sql =
                $"select * from {TableName} where traderid = @TraderId and id = @DocId and isdeleted = false";

            return await _postgresConnection.GetFirstRecordOrNullAsync<TraderDocument>(sql, new
            {
                TraderId = traderId,
                DocId = documentId
            });

        }

        public async ValueTask Add(TraderDocument itm)
        {
            var sql = $"insert into {TableName} (traderid, id, documenttype, datetime, mime, filename) values (@TraderId, @Id, @DocumentType, @DateTime, @Mime, @FileName)";

            await _postgresConnection.ExecAsync(sql, itm);

        }

        public async ValueTask DeleteAsync(string traderId, string docId)
        {

            var sql = $"update {TableName} set isdeleted = true where traderid = @TraderId and id = @Id";

            await _postgresConnection.ExecAsync(sql, new
            {
                TraderId = traderId,
                Id = docId
            });
        }
    }
}