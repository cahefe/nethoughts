using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Kash.Clients.WebAPI.DTO;
using Kash.Core.Models;
using Kash.Clients.WebAPI.Services;

namespace Kash.Clients.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        INegocioService _negocioService;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(INegocioService negocioService, ILogger<WeatherForecastController> logger)
        {
            _negocioService = negocioService;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public ActionResult<EntryReqDTO> Check([FromBody] EntryReqDTO entry)
        {
            var rng = new Random();
            Entry x = new Entry
            {
                ID = entry.ID,
                Value = entry.Value,
                FeesValue = entry.NewValue,
                Description = entry.Texto,
            };
            _negocioService.TratarErro(x);
            //  TODO: Consistir validação de modelo a partir da camada de serviço
            //Validator.ValidateObject(x, new ValidationContext(x));
            entry.NewValue = entry.Value * (decimal)rng.NextDouble();
            return Ok(entry);
        }
    }
}
