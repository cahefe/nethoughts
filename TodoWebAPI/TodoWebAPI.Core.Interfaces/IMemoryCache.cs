using System;
using System.Collections.Generic;
using System.Text;

namespace TodoWebAPI.Core.Interfaces
{
    public interface IMemoryCache
    {
        void Set(string key, object value, int lifetime);
    }
}
