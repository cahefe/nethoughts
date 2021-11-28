using System;
namespace Corp.System.Hexagonal.Orders.Domain.Model
{
    public class MarksInfo
    {
        public DateTime Register { get; set; } = DateTime.Now;
        public DateTime? Approval { get; set; }
        public DateTime? Reproval { get; set; }
        public DateTime? Cancel { get; set; }
        public DateTime? Settle { get; set; }
        public DateTime LastUpdate { get; set; } = DateTime.Now;
        public static MarksInfo NewMarks() => new MarksInfo();
    }
}
