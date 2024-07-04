using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Entities;

namespace CourseWork_OLX.Controllers
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
        private readonly IRepository<User> users;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IRepository<User> users)
        {
            _logger = logger;
            this.users = users;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task< IEnumerable<WeatherForecast>> Get()
        {
            await users.SaveAsync();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        
    }
}
