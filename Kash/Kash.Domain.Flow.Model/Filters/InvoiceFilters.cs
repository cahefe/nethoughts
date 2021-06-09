using System;
using Kash.Domain.Flow.Model;

namespace Kash.Domain.Flow.Model.Filters
{
    public class InvoiceFilters
    {
        public long InvoiceID { get; set; }
        public long AccoundID { get; set; }
        public EnumInvoiceType InvoiceType { get; set; } = EnumInvoiceType.Undefined;
        public EnumPaymentStatus PaymentStatus { get; set; } = EnumPaymentStatus.Undefined;
        public DateTime EntryDateBegin { get; set; } = DateTime.MinValue;
        public DateTime EntryDateEnd { get; set; } = DateTime.MaxValue;
        public short ReferenceDate { get; set; }
    }
}
