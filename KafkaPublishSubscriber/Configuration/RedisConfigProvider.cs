using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KafkaPublishSubscriber;

namespace KafkaPublishSubscriber.Configuration
{
    public class RedisConfigProvider : ConfigurationProvider
    {
        readonly IConfiguration _config;

        public RedisConfigProvider(IConfiguration config) => (_config) = (config);

        public override void Load()
        {
            //string redisServer = _config.GetValue<string>("RedisServer");
            var _cache = RedisConnectorHelper.Connection.GetDatabase();
            var _server = RedisConnectorHelper.Connection.GetServer(hostAndPort: "localhost:6379");

            foreach (var item in _server.Keys(pattern: "Key*"))
            {
                Data.Add(item, _cache.StringGet(item));
            }
        }
    }
}