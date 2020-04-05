using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class Document
    {
        public int ID  { get; set; }
        public int ClientID { get; set; }
        public EnumDocumentType Type { get; set; }
        public long Number { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Details { get; set; }

        [ForeignKey("ClientID")]
        public virtual Client Client { get; set; }
    }
}