using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public interface ICache
    {
        IList<string> AllKeys { get; }
        void Clean();
        object GetValue(string key);
        void Remove(string key);
        void SetValue(string key, object value);
        void SetValue(string key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration);
        //void Add(string key, object value, CacheExpirationTypes cacheExpirationType);

        //void Add(string key, object value, TimeSpan timeSpan);

        //void AddOrReplace(string key, object value, CacheExpirationTypes cacheExpirationType);

        //void AddOrReplace(string key, object value, TimeSpan timeSpan);

        //void AddWithFileDependency(string key, object value, string fullFileNameOfFileDependency);

        //void Clear();

        //object Get(string cacheKey);

        //Dictionary<string, Dictionary<string, string>> GetStatistics();

        //void MarkDeletion(string key, object value, CacheExpirationTypes cacheExpirationType);
    }
}
