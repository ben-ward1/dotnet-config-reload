using ConfigReload.Lib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ConfigReload.Function
{
    public class HttpFunction(
        IOptions<ConfigReloadOptions> options,
        IOptionsMonitor<ConfigReloadOptions> optionsMonitor,
        IConfiguration configuration
        )
    {
        [Function(nameof(HttpFunction))]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            var response = new
            {
                OptionsFoo = options.Value.Foo,
                OptionsMonitorFoo = optionsMonitor.CurrentValue.Foo,
                ConfigFoo = configuration["ConfigReloadSettings:Foo"]
            };

            return new OkObjectResult(response);
        }
    }
}
