using Microsoft.Extensions.Configuration;
using System;

namespace KafkaPublishSubscriber.Configuration
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddRedisConfigProvider(
            this IConfigurationBuilder configuration)
        {
            configuration.Add(new RedisConfigSource());
            return configuration;
        }
    }
}