using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutoStartWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger) => (_logger) = (logger);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
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
