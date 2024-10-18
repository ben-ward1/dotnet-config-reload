using ConfigReload.Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigReload.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(
        IOptions<ConfigReloadOptions> options,
        IOptionsMonitor<ConfigReloadOptions> optionsMonitor,
        IConfiguration configuration
        ) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var response = new
            {
                OptionsFoo = options.Value.Foo,
                OptionsMonitorFoo = optionsMonitor.CurrentValue.Foo,
                ConfigFoo = configuration["ConfigReloadSettings:Foo"]
            };

            return Ok(response);
        }
    }
}
