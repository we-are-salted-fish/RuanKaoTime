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
            throw new NotImplementedException();
        }

        public object GetValue(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void SetValue(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }
    }
}
