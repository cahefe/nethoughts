using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPIEnvironments.Attributes;
using WebAPIEnvironments.Interfaces;
using WebAPIEnvironments.Models;

namespace WebAPIEnvironments.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        static Random rng = new Random();
        static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        readonly ICalculosService _calculoService;
        readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ICalculosService calculosService, ILogger<WeatherForecastController> logger)
        {
            _calculoService = calculosService ?? throw new ArgumentException(nameof(calculosService));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        [HttpGet]
        [CenarioCertificacaoRelacionado(CenariosCertificacaoEnum.Registrar_Investimento_Titulo_Inexistente)]
        public IEnumerable<WeatherForecast> Get() => Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)],
            Calculo = _calculoService.CalcularSoma((decimal)rng.Next(), (decimal)rng.Next()),
        }).ToArray();
    }
}
