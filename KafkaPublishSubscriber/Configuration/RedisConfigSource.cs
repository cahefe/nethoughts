using Microsoft.Extensions.Configuration;
using System;

namespace KafkaPublishSubscriber.Configuration
{
    public class RedisConfigSource : IConfigurationSource
    {
        // private readonly Action<DbContextOptionsBuilder> _optionsAction;

        // public RedisConfigSource(Action<DbContextOptionsBuilder> optionsAction)
        // {
        //     _optionsAction = optionsAction;
        // }
        readonly IConfiguration _config;

        public IConfigurationProvider Build(IConfigurationBuilder builder) => new RedisConfigProvider(_config);
    }
}