using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StructureMap;
// using StructureMap.Microsoft.DependencyInjection;

namespace ConsoleHangFire
{
    class Program
    {
        static IServiceProvider _serviceProvider;
        static ILogger<Program> _logger;

        static void Main(string[] args)
        {
            ConfigureServices(args);
            var bar = _serviceProvider.GetService<IBarService>();
            bar.DoSomeRealWork();
            _logger.LogDebug("All done!");
        }
        static void ConfigureServices(string[] args)
        {
            var services = new ServiceCollection()
            .AddLogging(options =>
            {
                options.AddConsole().SetMinimumLevel(LogLevel.Debug);
            });

            // add StructureMap
            var container = new Container(config =>
            {
                // Register stuff in container, using the StructureMap APIs...
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(Program));
                    _.WithDefaultConventions();
                });
                //  Popula o container com a coleção a partir de uma coleção qualquer
                config.Populate(services);
            });
            //  Obtem a instância do ServiceProvider...
            _serviceProvider = container.GetInstance<IServiceProvider>();

            _logger = _serviceProvider.GetService<ILogger<Program>>();
            _logger.LogDebug("Application configured");
        }
        static void ConfigureServicesNoStructuredMap(string[] args)
        {
            _serviceProvider = new ServiceCollection()
            .AddLogging(options =>
            {
                options.AddConsole().SetMinimumLevel(LogLevel.Debug);
            })
            .AddSingleton<IFooService, FooService>()
            .AddSingleton<IBarService, BarService>()
            .BuildServiceProvider();

            _logger = _serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            _logger.LogDebug("Application configured");
        }
    }
    public interface IFooService
    {
        void DoThing(int number);
    }

    public interface IBarService
    {
        void DoSomeRealWork();
    }
    public class BarService : IBarService
    {
        private readonly IFooService _fooService;
        public BarService(IFooService fooService)
        {
            _fooService = fooService ?? throw new ArgumentNullException(nameof(fooService));
        }

        public void DoSomeRealWork()
        {
            for (int i = 0; i < 10; i++)
            {
                _fooService.DoThing(i);
            }
        }
    }
    public class FooService : IFooService
    {
        readonly ILogger<FooService> _logger;
        public FooService(ILogger<FooService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void DoThing(int number)
        {
            _logger.LogInformation($"Doing the thing {number}");
        }
    }
}
