using Microsoft.AspNetCore.Mvc;

namespace poc_elk.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            try
            {

                if (DateTime.Now.Second % 2 == 0)
                    throw new Exception("Ocorreu um erro");

                return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado.");
                return new StatusCodeResult(500);
            }            
        }
    }
}