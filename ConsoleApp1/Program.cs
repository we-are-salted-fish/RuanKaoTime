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

            Console.ReadKey();
        }
    }
}
