using System;

namespace Kash.Core.Models
{
    [Flags]
    public enum AccountTypeEnum : byte
    {
        Undefined = 0,
        Checking = 2^0,
        Savings = 2^1,
        Credit = 2^2,
        Debit = 2^3
    }
}
