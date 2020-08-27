using System;

namespace Swisschain.PersonalData.Postgres
{
    public class TraderDocument
    {
        public string TraderId { get; set; }

        public string Id { get; set; }

        public int DocumentType { get; set; }

        public DateTime DateTime { get; set; }

        public string Mime { get; set; }
        
        public string FileName { get; set; }
    }

}