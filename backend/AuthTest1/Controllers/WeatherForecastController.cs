using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthTest1.Controllers
{
    [ApiController]
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

        //[Authorize]
        [HttpGet]
        [Route("")]
        public ContentResult Get()
        {
#if DEBUG
            var s = "";
#else
            var s =System.IO.File.ReadAllText( Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/index.html"));
#endif
            return new ContentResult
            {
                ContentType = "text/html",
                Content = s
            };
        }
    }
}
