﻿using System;
using StackExchange.Redis;

namespace ConsoleRedis
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379"))
            {
                var server = redis.GetServer("localhost:6379");
                foreach (var key in server.Keys(pattern: "mykey*"))
                {
                    Console.WriteLine(key);
                }

                IDatabase db = redis.GetDatabase();
                for (int i = 0; i < 5; i++)
                {
                    db.StringSet($"mykey+{i}", $"value+{r.Next(100)}");
                    var k = db.StringGet(new RedisKey($"mykey+{i}"), CommandFlags.None);
                    Console.WriteLine("k:" + k ?? "nulo");
                }
            }
        }
    }
}
