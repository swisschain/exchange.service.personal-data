using Swisschain.Domains.PersonalData;
using Swisschain.PersonalData.Grpc.Models;
using Swisschain.PersonalData.Postgres;

namespace Swisschain.PersonalData.Server.Mappers
{
    public static class PersonalDataGrpcMapper
    {
        public static PersonalDataGrpcModel ToGrpcModel(this PersonalDataPostgresEntity src)
        {
            if (src == null)
                return null;
            
            return new PersonalDataGrpcModel
            {
                Id = src.Id,
                Email = src.Email,
                FirstName = src.FirstName,
                LastName = src.LastName,
                DateOfBirth = src.DateOfBirth,
                City = src.City,
                Phone = src.Phone,
                Sex = src.GetPersonalDataSex(),
                PostalCode = src.PostalCode,
                Address = src.Address,
                CountryOfCitizenship = src.CountryOfCitizenship,
                CountryOfResidence = src.CountryOfResidence,
                Kyc = src.GetKycStatus(),
                Confirm = src.Confirm
            };
        }
        
        public static PersonalDataPostgresEntity ToDomainModel(this RegisterPersonalDataGrpcModel src, bool isInternal)
        {
            if (src == null)
                return null;

            return new PersonalDataPostgresEntity
            {
                Id = src.Id,
                Email = src.Email,
                IsInternal = isInternal,
                CountryOfResidence = src.CountryOfResidence,
                CountryOfRegistration = src.CountryOfRegistration,
                IpOfRegistration = src.IpOfRegistration,
                KYC = (int)PersonalDataKycStatus.NotVerified
            };
        }
        
        public static PersonalDataPostgresEntity ToPostgresEntity(this PersonalDataGrpcModel src)
        {
            if (src == null)
                return null;

            return new PersonalDataPostgresEntity
            {
                Id = src.Id,
                Email = src.Email,
                FirstName = src.FirstName,
                LastName = src.LastName,
                DateOfBirth = src.DateOfBirth,
                City = src.City,
                Phone = src.Phone,
                Sex = (int)src.Sex,
                PostalCode = src.PostalCode,
                CountryOfCitizenship = src.CountryOfCitizenship,
                CountryOfResidence = src.CountryOfResidence,
                KYC = (int)src.Kyc,
                Confirm = src.Confirm,
                Address = src.Address,
            };
        }
    }
}