using RedisSomeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class RedisCache : ICache
    {
        public IList<string> AllKeys => PooledRedisClientHelper.GetAllKeys();

        public void Clean()
        {
            foreach (var key in AllKeys)
            {
                PooledRedisClientHelper.Remove(key);
            }
        }

        public object GetValue(string key)
        {
            if (PooledRedisClientHelper.ContainsKey(key))
            {
                return PooledRedisClientHelper.GetValueString(key);
            }

            return null;
        }

        public void Remove(string key)
        {
            if (PooledRedisClientHelper.ContainsKey(key))
            {
               PooledRedisClientHelper.Remove(key);
            }
        }

        public void SetValue(string key, object value)
        {
            PooledRedisClientHelper.SetT<object>(key, value);
        }

        public void SetValue(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            PooledRedisClientHelper.SetT<object>(key, value);
        }
    }
}
