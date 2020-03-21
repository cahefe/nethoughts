using Microsoft.Extensions.Logging;

namespace KafkaPublishSubscriber
{
    public class PersonService
    {
        readonly ILogger _logger;

        public PersonService(ILoggerFactory loggerFactory) => _logger = loggerFactory.CreateLogger<PersonService>();

        public void DoWork(string whatWork) {
             _logger.LogTrace(whatWork + " Trace");
             _logger.LogDebug(whatWork + " Debug");
             _logger.LogInformation(whatWork + " Info");
             _logger.LogWarning(whatWork + " Worning");
             _logger.LogError(whatWork + " Error");
             _logger.LogCritical(whatWork + " Critical");
        }
    }
}