using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Document.Library.Globals
{
    public static class CacheManager
    {
        private static ObjectCache cache = MemoryCache.Default;
        private static Object mutex = new Object();

        public static void Set(String key, Object item, string region, CacheItemPolicy cacheItemPolicy = null)
        {
            string compositeKey = region + "-" + key;

            if (cache[compositeKey] == null)
            {
                if (cacheItemPolicy == null)
                {
                    cacheItemPolicy = new CacheItemPolicy();
                    cacheItemPolicy.SlidingExpiration = Constants.CacheSlidingExpirationTimeSpan;
                }
                cache.Set(compositeKey, item, cacheItemPolicy);
            }
            else
            {
                cache[compositeKey] = item;
            }
        }

        public static Object Get<T>(string key, string region)
        {
            string compositeKey = region + "-" + key;
            return (T)cache[compositeKey];
        }

        public static Object Get(string key, string region)
        {
            string compositeKey = region + "-" + key;
            return cache[compositeKey];
        }

        public static bool ContainsKey(String key, String region)
        {
            string compositeKey = region + "-" + key;
            return cache.Contains(compositeKey);
        }

        public static void Remove(String key, String region)
        {
            string compositeKey = region + "-" + key;
            if (ContainsKey(key, region))
            {
                cache.Remove(compositeKey);
            }
        }
    }

}
