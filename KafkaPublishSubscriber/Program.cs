using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using KafkaPublishSubscriber.FileLoggerProvider;
using System.Threading;
using System.Text.Json;
using StackExchange.Redis;

namespace KafkaPublishSubscriber
{
    class Program
    {
        static bool isProcessing = true;
        static CancellationTokenSource _cts = new CancellationTokenSource();
        static ILogger _logger;
        static IDatabase _cache;
        static void Main(string[] args)
        {
            var builtConfig = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            //.AddCommandLine(args)
            .Build();
            //  Consultar: https://www.codeproject.com/Articles/1556475/How-to-Write-a-Custom-Logging-Provider-in-ASP-NET e https://docs.microsoft.com/pt-br/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1
            //  Detalhes de log: https://docs.microsoft.com/pt-br/archive/msdn-magazine/2016/april/essential-net-logging-with-net-core
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddConfiguration(builtConfig.GetSection("Logging"))
                    .ClearProviders()
                    .AddConsole(c =>
                    {
                        c.TimestampFormat = "[dd/MM/yy HH:mm:ss.fff] ";
                    })
                    .AddFile(f =>
                    {
                        f.Extension = "log";
                        f.FileName = "app_info";
                        f.FileSizeLimit = 4096;
                        f.LogDirectory = "./logs";
                        f.BatchSize = 5;
                        f.Periodicity = PeriodicityOptions.Minutely;
                        f.IsEnabled = true;
                    });
            });
            _logger = loggerFactory.CreateLogger<Program>();

            UseRedis();
            return;



            List<Task> TaskList = new List<Task>();
            for (int x = 0; x < 10; x++)
            {
                var TaskItem = new Task(GoConsume);
                TaskItem.Start();
                TaskList.Add(TaskItem);
                // TaskItem = new Task(GoProduce);
                // TaskItem.Start();
                // TaskList.Add(TaskItem);
            }
            Console.CancelKeyPress += (_, e) =>
                            {
                                e.Cancel = true; // prevent the process from terminating.
                                isProcessing = false;
                                _cts.Cancel();
                            };
            Task.WaitAll(TaskList.ToArray());
        }

        /// https://www.c-sharpcorner.com/UploadFile/2cc834/using-redis-cache-with-C-Sharp/
        static void UseRedis()
        {
            _cache = RedisConnectorHelper.Connection.GetDatabase();
            _logger.LogInformation("Saving random data in cache");
            CacheSaveBigData();

            _logger.LogInformation("Reading data from cache");
            CacheReadData();

            _logger.LogInformation("Finished");
        }
        static void CacheReadData()
        {
            int devicesCount = Get("devicesCount", -1);
            for (int i = 0; i < devicesCount; i++)
            {
                int value = Get<int>($"Device_Status:{i}", -1);
                _logger.LogInformation($"Valor={value}");
            }
        }

        static void CacheSaveBigData()
        {
            var rnd = new Random();
            var devicesCount = 10;

            Set<int>("devicesCount", devicesCount, 60);

            for (int i = 0; i < devicesCount; i++)
            {
                var value = rnd.Next(0, 10000);
                Set<int>($"Device_Status:{i}", value, 20);
            }
        }
        static void Set<T>(string key, object value, int seconds)
        {
            //var cache = RedisConnectorHelper.Connection.GetDatabase();
            _cache.StringSet(key, JsonSerializer.Serialize(value, typeof(T)), new TimeSpan(0, 0, seconds));
        }
        static T Get<T>(string key, T defaultValue)
        {
            var value = _cache.StringGet(key);
            if (value.HasValue)
                return (T)JsonSerializer.Deserialize(value, typeof(T));
            return defaultValue;
        }

        static async void GoProduce()
        {
            int cont = 0;
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092",
                ClientId = Dns.GetHostName(),
            };
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
                while (isProcessing)
                    try
                    {
                        var dr = await producer.ProduceAsync("weblog", new Message<Null, string> { Value = "Log info (" + cont++ + "): " + Guid.NewGuid() });
                        _logger.LogInformation($"Delivered \"{dr.Value}\" to {dr.TopicPartitionOffset}");
                    }
                    catch (ProduceException<Null, string> ex)
                    {
                        _logger.LogError($"Delivery failed: {ex.Error.Reason}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Delivery failed: {ex.Message}");
                    }
            _logger.LogInformation($"Messages produced: {cont}");
        }
        static void GoConsume()
        {
            int cont = 0;
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };
            // https://docs.confluent.io/current/clients/dotnet.html
            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe("weblog");
                try
                {
                    while (isProcessing)
                        try
                        {
                            // ConsumeResult<Ignore, string> cr = consumer.Consume(new TimeSpan(0, 0, 2));
                            ConsumeResult<Ignore, string> cr = consumer.Consume(_cts.Token);
                            _logger.LogInformation($"Consumed message ({++cont}) \"{cr.Value}\" at {cr.TopicPartitionOffset}");
                        }
                        catch (ConsumeException ex)
                        {
                            _logger.LogError($"Consumer error occurred: {ex.Error.Reason}");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"Consumer generic error occurred: {ex.Message}");
                        }
                }
                catch (OperationCanceledException ex)
                {
                    _logger.LogError($"Consumer cancel error occurred: {ex.Message}");
                }
                finally
                {
                    consumer.Close();
                }
            }
            _logger.LogInformation($"Messages consumed: {cont}");
        }
    }
}
