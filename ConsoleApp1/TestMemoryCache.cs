using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace ConsoleApp1
{
    public class TestMemoryCache : ICache
    {
        private MemoryCache cache = MemoryCache.Default;
        public IList<string> AllKeys => cache.Select(x => x.Key).ToList();

        public void Clean()
        {
            foreach (var key in AllKeys)
            {
                cache.Remove(key);
            }
        }

        public object GetValue(string key)
        {
            return cache.Get(key);
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public void SetValue(string key, object value)
        {
            cache.Add(key, value, DateTime.Now.AddHours(6));
        }

        public void SetValue(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            cache.Add(key, value, absoluteExpiration);
        }
    }
}
