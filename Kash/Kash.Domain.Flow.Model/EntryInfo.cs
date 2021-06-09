using System;

namespace Kash.Domain.Flow.Model
{
    public abstract class EntryInfo
    {
        public long CreateUserID { get; set; }
        public long ModifyUserID { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime ModifyDate { get; set; } = DateTime.Now;
        public virtual User CreateUser { get; set; }
        public virtual User ModifyUser { get; set; }
    }
}
