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
        static Random rnd = new Random();
        static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        readonly ICalculosService _calculoService;
        readonly IWideServiceFactory<ITextos> _textosServiceFactory;
        readonly IWideServiceFactory<INumeros> _numerosServiceFactory;
        readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ICalculosService calculosService, IWideServiceFactory<ITextos> textosServiceFactory, IWideServiceFactory<INumeros> numerosServiceFactory, ILogger<WeatherForecastController> logger)
        {
            _calculoService = calculosService ?? throw new ArgumentException(nameof(calculosService));
            _textosServiceFactory = textosServiceFactory ?? throw new ArgumentException(nameof(textosServiceFactory));
            _numerosServiceFactory = numerosServiceFactory ?? throw new ArgumentException(nameof(numerosServiceFactory));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        [HttpGet]
        [CenarioCertificacaoRelacionado(CenariosCertificacaoEnum.Registrar_Investimento_Titulo_Inexistente)]
        public IEnumerable<WeatherForecast> Get() => Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Numeros = _numerosServiceFactory.GetService(rnd.Next(2)).GerarNumero(rnd.Next(10, 19)),
            Textos = _textosServiceFactory.GetService(rnd.Next(10) % 2 == 0 ? 'F' : 'X').GerarTexto(rnd.Next(30, 50)),
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rnd.Next(-20, 55),
            Summary = Summaries[rnd.Next(Summaries.Length)],
            Calculo = _calculoService.CalcularSoma((decimal)rnd.Next(10, 20), (decimal)rnd.Next(100, 200)),
            TextoAleatorio = _calculoService.GerarTexto(rnd.Next(1, 4))
        }).ToArray();
    }
}
