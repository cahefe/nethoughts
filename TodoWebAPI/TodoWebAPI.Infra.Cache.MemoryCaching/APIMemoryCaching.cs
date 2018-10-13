using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;

namespace TodoWebAPI.Infra.Cache.MemoryCaching
{
    public class APIMemoryCaching : MemoryCache, Core.Interfaces.IMemoryCache
    {
        public APIMemoryCaching(IOptions<MemoryCacheOptions> optionsAccessor) : base(optionsAccessor) { }
        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();

        public void Set(string key, object value, int lifetime)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
