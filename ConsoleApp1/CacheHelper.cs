using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public static class CacheHelper
    {
        private static ICache cache;
        private static readonly object lockKey = new object();
        static CacheHelper()
        {
            //cache = ObjectHelper.GetObject<ICache>();
            //cache = new RedisCache();
            cache = new TestMemoryCache();
        }

        public static IList<string> AllKeys
        {
            get
            {
                return cache.AllKeys;
            }
        }

        public static void SetValue(string key, object value)
        {
            lock (lockKey)
            {
                if (GetValue(key) != null)
                {
                    Remove(key);
                }
                cache.SetValue(key, value);
            }
        }

        public static void SetValue(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            lock (lockKey)
            {
                if (GetValue(key) != null)
                {
                    Remove(key);
                }
                cache.SetValue(key, value, absoluteExpiration, slidingExpiration);
            }
        }

        public static object GetValue(string key)
        {
            return cache.GetValue(key);
        }


        public static bool Remove(string key)
        {
            if (cache.GetValue(key) != null)
            {
                cache.Remove(key);
                return true;
            }
            return false;
        }

        public static void Clean()
        {
            lock (lockKey)
            {
                foreach (var key in cache.AllKeys)
                {
                    cache.Remove(key);
                }
            }
        }

        public static T GetFromCache<T>(string key, Func<T> fun) where T : class
        {
            T returnValue = GetValue(key) as T;
            if (returnValue == null)
            {
                lock (lockKey)
                {
                    if (returnValue == null)
                    {
                        returnValue = fun();
                        if (returnValue != null && !string.IsNullOrEmpty(returnValue.ToString()))
                        {
                            SetValue(key, returnValue);
                        }
                    }
                }
            }
            return returnValue;
        }


        public static T GetFromCache<T>(string key, Func<T> fun, DateTime absoluteExpiration, TimeSpan slidingExpiration) where T : class
        {
            T returnValue = GetValue(key) as T;
            if (returnValue == null)
            {
                lock (lockKey)
                {
                    if (returnValue == null)
                    {
                        returnValue = fun();
                        if (returnValue != null && !string.IsNullOrEmpty(returnValue.ToString()))
                        {
                            SetValue(key, returnValue, absoluteExpiration, slidingExpiration);
                        }
                    }
                }
            }
            return returnValue;
        }


        public static T GetFromCache<T>(string key, int CacheSeconds, Func<T> fun) where T : class
        {
            T returnValue = GetValue(key) as T;
            if (returnValue == null)
            {
                lock (lockKey)
                {
                    if (returnValue == null)
                    {
                        returnValue = fun();
                        if (returnValue != null && !string.IsNullOrEmpty(returnValue.ToString()))
                        {
                            SetValue(key, returnValue, DateTime.Now.AddSeconds(CacheSeconds), TimeSpan.Zero);
                        }
                    }
                }
            }
            return returnValue;
        }
    }

}
