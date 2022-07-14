using RedisSomeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            PooledRedisClientHelper.Set("bbb", "111", DateTime.Now.AddMinutes(10));

            var keys = CacheHelper.AllKeys;

            foreach (var key in keys)
            {
                Console.WriteLine($"key:{key}");
            }

            Console.ReadKey();
        }
    }
}
