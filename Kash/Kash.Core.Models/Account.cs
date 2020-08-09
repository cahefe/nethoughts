using System;

namespace Kash.Core.Models
{
    public class Account
    {
        public short ID { get; set; }
        public AccountTypeEnum Type { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
