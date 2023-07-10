using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using net6_web_api.DB;

namespace net6_web_api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    //[EnableCors("Any")] //�����������
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly SchoolContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, SchoolContext context)
        {
            _logger = logger;
            _context = context;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("i am log");

            //��ѯ
            var Students = _context.Students.ToList();
            var stutdent = Students[0];
            stutdent.FirstMidName = "111";
            stutdent.LastName = "222";
            //����
            _context.Students.Update(stutdent);

            //�������޸���Ҫ�����������Ч
            _context.SaveChanges();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}