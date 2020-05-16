using AutoStartWorkerService.Interfaces;
using AutoStartWorkerService.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutoStartWorkerService
{
    //  Ref: https://medium.com/@nickfane/introduction-to-worker-services-in-net-core-3-0-4bb3fc631225
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<EventContext>(opt => opt.UseInMemoryDatabase(databaseName: "Test"));
                    services.AddScoped<IEventsRepo, EventsRepo>();
                    services.AddHostedService<Worker>();
                });
    }
}
