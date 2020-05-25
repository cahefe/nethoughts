using System;
using Kash.Core.Models.Validations;

namespace Kash.Core.Models
{
    [BR0003_SomaDeveTerValorMinimoAttribute(20)]
    public class Entry
    {
        public short ID { get; set; }
        public EntryTypeEnum Type { get; set; }
        public short SupplierID { get; set; }
        [BR0001_ValorDeveSerMaiorQueParametro]
        public decimal Value { get; set; }
        public decimal ExtraValue { get; set; }
        public decimal FeesValue { get; set; }
        public decimal TicketValue { get; set; }
        public decimal TotalValue { get; set; }
        [BR0002_QuantidadeMaximaCaracteresExedida(5)]
        public string Description { get; set; }
        public EntrySituationEnum Situation { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime SettleDate { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
