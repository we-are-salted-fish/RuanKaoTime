using RedisSomeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //RedisCacheTest();

            MemoryCacheTest();

            Console.ReadKey();
        }

        static void RedisCacheTest()
        {
            PooledRedisClientHelper.Set("bbb", "111", DateTime.Now.AddMinutes(10));

            var b = new Bread();
            b.Name = "Hello";

            CacheHelper.SetValue("bread", JsonConvert.SerializeObject(b));

            var t = JToken.Parse(CacheHelper.GetValue("bread") as string).ToString();

            Console.WriteLine($"t:{t}");

            var tt = JsonConvert.DeserializeObject<Bread>(t);

            Console.WriteLine($"tt:{JsonConvert.SerializeObject(tt)}");

            var keys = CacheHelper.AllKeys;

            foreach (var key in keys)
            {
                Console.WriteLine($"key:{key}");
            }
        }

        static void MemoryCacheTest()
        {
            var b = new Bread();
            b.Name = "Hello";

            CacheHelper.SetValue("bread", b);

            var t = JsonConvert.SerializeObject(CacheHelper.GetValue("bread"));

            Console.WriteLine($"t:{t}");

            var keys = CacheHelper.AllKeys;

            foreach (var key in keys)
            {
                Console.WriteLine($"key:{key}");
            }
        }
    }

    class Bread
    {
        public string Name { get; set; }
    }
}
