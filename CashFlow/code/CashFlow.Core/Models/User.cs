using System;

namespace CashFlow.Core.Models
{
    public class User
    {
        public long ID { get; set; }
        public string Login { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
