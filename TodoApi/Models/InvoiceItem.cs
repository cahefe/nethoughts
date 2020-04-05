using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Models
{
    public class InvoiceItem
    {
        public int ID { get; set; }
        public int InvoiceId { get; set; }
        public string Code { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
    }
}