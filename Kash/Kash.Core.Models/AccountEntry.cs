using System;

namespace Kash.Core.Models
{
    public class AccountEntry
    {
        public short AccountID { get; set; }
        public short EntryID { get; set; }
        public AccountEntrySituationEnum Situation { get; set; } = AccountEntrySituationEnum.Linked;
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public virtual Account Account { get; set; }
        public virtual Entry Entry { get; set; }
    }
}
