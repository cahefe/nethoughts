using System;

namespace TodoApi.Models
{
    public class Document
    {
        public int ID  { get; set; }
        public EnumDocumentType Type { get; set; }
        public long Number { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Details { get; set; }
    }
}