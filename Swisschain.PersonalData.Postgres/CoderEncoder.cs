using System.Security.Cryptography;
using System.Text;
using Swisschain.Cryptography;

namespace Swisschain.PersonalData.Postgres
{
    public static class CoderEncoder
    {
        public static void Encode(this PersonalDataPostgresEntity entity, byte[] key)
        {
            entity.City = entity.City?.EncodeString(key);
            entity.Email = entity.Email?.EncodeString(key);
            entity.Phone = entity.Phone?.EncodeString(key);
            entity.FirstName = entity.FirstName?.EncodeString(key);
            entity.LastName = entity.LastName?.EncodeString(key);
            entity.PostalCode = entity.PostalCode?.EncodeString(key);
            entity.CountryOfCitizenship = entity.CountryOfCitizenship?.EncodeString(key);
            entity.CountryOfResidence = entity.CountryOfResidence?.EncodeString(key);
            entity.Address = entity.Address?.EncodeString(key);
        }

        public static void Decode(this PersonalDataPostgresEntity entity, byte[] key)
        {
            entity.City = entity.City?.DecodeString(key);
            entity.Email = entity.Email?.DecodeString(key);
            entity.Phone = entity.Phone?.DecodeString(key);
            entity.FirstName = entity.FirstName?.DecodeString(key);
            entity.LastName = entity.LastName?.DecodeString(key);
            entity.PostalCode = entity.PostalCode?.DecodeString(key);
            entity.CountryOfCitizenship = entity.CountryOfCitizenship?.DecodeString(key);
            entity.CountryOfResidence = entity.CountryOfResidence?.DecodeString(key);
            entity.Address = entity.Address?.DecodeString(key);
        }
        
        public static string EncodeToSha1(this string str)
        {
            using var sha1 = SHA1.Create();
            var hashBytes = sha1.ComputeHash(Encoding.ASCII.GetBytes(str));
            return hashBytes.ToHexString();
        }

        public static string EncodeString(this string str, byte[] key)
        {
            var data = Encoding.UTF8.GetBytes(str);

            var result = AesEncodeDecode.Encode(data, key);
            
            return result.ToHexString();
        }

        public static string DecodeString(this string str, byte[] key)
        {
            var data = str.HexStringToByteArray();
            
            return Encoding.UTF8.GetString(AesEncodeDecode.Decode(data, key));
        }
    }
}