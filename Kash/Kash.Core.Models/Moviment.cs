using System;

namespace Kash.Core.Models
{
    public class Moviment
    {
        public short AccountFromID { get; set; }
        public short AccountToID { get; set; }
        public short EntryID { get; set; }
        public MovementTypeEnum Type { get; set; }
        public decimal Value { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual Account AccountFrom { get; set; }
        public virtual Account AccountTo { get; set; }
        public Entry Entry { get; set; }
    }
}
