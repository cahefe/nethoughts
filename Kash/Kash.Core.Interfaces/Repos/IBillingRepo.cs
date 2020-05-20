using System;
using Kash.Core.Models;

namespace Kash.Core.Interfaces.Repos
{
    public interface IBillingRepo
    {
        void Credit(Entry entry);
    }
}
