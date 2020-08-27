using System;
using System.IO;
using Swisschain.PersonalData.Grpc.Contracts;
using Swisschain.PersonalData.Grpc.Models;
using Swisschain.PersonalData.Postgres;

namespace Swisschain.PersonalData.Server.Mappers
{
    public static class DocumentsMapper
    {
        public static string ConvertFileNameToMime(this string fileName)
        {
            var ext = Path.GetExtension(fileName).ToLower();

            switch (ext)
            {
                case ".jpeg":
                case ".jpg":
                    return "image/jpeg";
                case ".pdf":
                    return "application/pdf";
                case ".png":
                    return "image/png";
                default:
                    throw new Exception("Unsupported File Extension");
            }
        }
        
        public static TraderDocument ToDomain(this UploadDocumentGrpcContract src)
        {
            return new TraderDocument
            {
                Id = Guid.NewGuid().ToString("N"),
                Mime = src.FileName.ConvertFileNameToMime(),
                DateTime = DateTime.UtcNow,
                DocumentType = (int) src.DocumentType,
                TraderId = src.TraderId,
                FileName = src.FileName
            };
        }

        public static TraderDocumentGrpcModel ToGrpc(this TraderDocument document)
        {
            return new TraderDocumentGrpcModel
            {
                TraderId = document.TraderId,
                Id = document.Id,
                DocumentType = (DocumentType) document.DocumentType,
                DateTime = document.DateTime,
                Mime = document.Mime,
                FileName = document.FileName
            };
        }
    }
}