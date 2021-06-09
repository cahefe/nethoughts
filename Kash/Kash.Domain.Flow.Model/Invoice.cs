using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kash.Domain.Flow.Model
{
    public class Invoice : EntryInfo
    {
        [Key]
        public long ID { get; set; }
        public long AccountID { get; set; }
        [Column("InvoiceType")]
        public EnumInvoiceType Type { get; set; } = EnumInvoiceType.Undefined;
        [Column("InvoiceStatus")]
        public EnumPaymentStatus Status { get; set; } = EnumPaymentStatus.Pending;
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }
        public short ReferenceDate { get; set; } //  eg: 2104 = (apr/2021)
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; } = DateTime.MaxValue;
        public decimal Value { get; set; }
        public decimal Fees { get; set; }
        public decimal Fines { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public virtual Account Account { get; set; }
    }
}
