using EasyCaching.Core;
using Microsoft.AspNetCore.Mvc;
using TestEasyCaching.Services;

namespace TestEasyCaching.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDemoService _demoService;

        private readonly IEasyCachingProvider _provider;



        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDemoService demoService, IEasyCachingProvider provider)
        {
            _logger = logger;
            _demoService = demoService;
            _provider = provider;
        }


        [HttpGet]
        [Route("GetTest")]
        public string GetTest(int type,string saveData)
        {
            if (type == 1)
            {
                return _demoService.GetCurrentUtcTime();
            }
            else if (type == 2)
            {
                _demoService.DeleteSomething(1);
                return "ok";
            }
            else if (type == 3)
            {
                return _demoService.PutSomething(saveData);
            }
            else
            {
                return "other";
            }
        }


        [HttpGet]
        [Route("GetSomething")]
        public string GetSomething(int type)
        {
            _provider.Set("demo", "123", TimeSpan.FromMinutes(1));

            return "ok";
        }


      
    }
}
