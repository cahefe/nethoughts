using System;
using System.Threading;
using System.Threading.Tasks;
using AutoStartWorkerService.Interfaces;
using AutoStartWorkerService.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoStartWorkerService
{
    public class Worker : BackgroundService
    {
        readonly ILogger<Worker> _logger;
        readonly IServiceScopeFactory _serviceFactory;

        public Worker(IServiceScopeFactory serviceFactory, ILogger<Worker> logger)
        {
            _serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var rand = new Random();
            using (var scope = _serviceFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<EventContext>();
                IEventsRepo eventsRepo = scope.ServiceProvider.GetRequiredService<IEventsRepo>();
                var eventID = eventsRepo.Create(new EventDetail
                {
                    Price = (decimal)rand.NextDouble(),
                    Description = $"Texto {rand.Next(10, 60)}"
                });
                var getEvent = eventsRepo.Get(eventID);
                _logger.LogInformation($"Event: {getEvent.ID} - {getEvent.Moment} => " + getEvent.Description);
            }
            Random rnd = new Random();
            double cont = 0;
            double valor = 0;
            double min = double.MaxValue;
            double max = double.MinValue;
            while (!stoppingToken.IsCancellationRequested)
            {
                cont = rnd.Next();
                try
                {
                    valor = rnd.Next() / cont;
                    min = valor < min ? valor : min;
                    max = !double.IsInfinity(valor) && valor > max ? valor : max;
                }
                catch
                {
                    _logger.LogError("Fail devide by {cont}", cont);
                }
                _logger.LogInformation("loop {cont} running at {time:dd/MM/yy HH:mm:ss.fff}: result {valor:N4} ({min:N4}/{max:N4})", cont++, DateTimeOffset.Now, valor, min, max);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
