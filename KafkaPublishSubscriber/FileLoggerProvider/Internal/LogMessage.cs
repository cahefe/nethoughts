using System;

namespace KafkaPublishSubscriber.FileLoggerProvider.Internal
{
    public struct LogMessage
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Message { get; set; }
    }
}