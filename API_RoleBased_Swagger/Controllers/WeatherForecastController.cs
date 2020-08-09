using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace API_RoleBased_Swagger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        readonly IDistributedCache _cache;
        readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IDistributedCache cache, ILogger<WeatherForecastController> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        [AppProfiles(EnumAppProfiles.Public)]
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

        // [AppProfiles(EnumAppProfiles.Users)]
        // [Authorize(Roles = "MyCustomRole")]
        [HttpGet("{id}")]
        public async System.Threading.Tasks.Task<ActionResult<WeatherForecast>> GetAsync(int id)
        {
            var rng = new Random();
            var cacheID = await _cache.GetAsync("id");
            var cacheVal = await _cache.GetAsync("val");
            return Ok(new WeatherForecast
            {
                Date = DateTime.Now.AddDays(id),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                CacheID = cacheID == null ? -1 : Convert.ToInt32(cacheID),
                CacheVal = cacheVal?.ToString()
            });
        }
        [HttpPost("{id}/{val}")]
        public ActionResult<WeatherForecast> PostCache(int id, string val)
        {
            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1));
            _cache.Set("id", Encoding.UTF8.GetBytes(id.ToString()), options);
            _cache.Set("val", Encoding.UTF8.GetBytes(val), options);
            return Ok(new { id, val });
        }
    }
}
