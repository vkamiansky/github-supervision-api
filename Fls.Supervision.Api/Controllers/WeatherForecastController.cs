using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenTracing;

namespace Fls.Supervision.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        // private readonly ITracer _tracer;

        public WeatherForecastController(ILogger<WeatherForecastController> logger) //, ITracer tracer)
        {
            _logger = logger;
            // _tracer = tracer;
        }

        [HttpGet]
        [Route("ping")]
        public IEnumerable<WeatherForecast> Get()
        {
            ITracer tracer = null;
            try{
                tracer = JaegerHelper.CreateTracer();

            }
            catch(Exception ex)
            {

            }
            return  JaegerHelper.Trace(tracer, "get-weather-ping", () =>
            {
                throw new Exception("Errrrrrrrorrrrrr!!!!!!");
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            });         
        }

        public class TestPingParams
        {
            
            public string Message { get; set; }
        }

        [HttpPost]
        [Route("test")]
        public IActionResult Post([FromForm]TestPingParams param)
        {
            return Ok(param);
        }
    }
}
