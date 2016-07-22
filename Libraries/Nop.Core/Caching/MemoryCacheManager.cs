using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Nop.Core.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        /// <summary>
        /// Cache object
        /// </summary>
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public T Get<T>(string key, Func<T> acquire)
        {
            return Get(key, 60, acquire);
        }

        public T Get<T>(string key, int cacheTime, Func<T> acquire)
        {
            if (this.IsSet(key))
            {
                return this.Get<T>(key);
            }

            var result = acquire();
            if (cacheTime > 0)
                this.Set(key, result, cacheTime);
            return result;
        }

        public void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy();
            
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }

        public bool IsSet(string key)
        {
            return (Cache.Contains(key));
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = new List<String>();

            foreach (var item in Cache)
                if (regex.IsMatch(item.Key))
                    keysToRemove.Add(item.Key);

            foreach (string key in keysToRemove)
            {
                Remove(key);
            }
        }

        public void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }
    }
}
