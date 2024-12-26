using ConfigReload.Lib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Options;

namespace ConfigReload.Function
{
    public class HttpFunction(IOptionsMonitor<ConfigReloadOptions> optionsMonitor)
    {
        [Function(nameof(HttpFunction))]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            var response = new
            {
                optionsMonitor.CurrentValue.Foo,
                optionsMonitor.CurrentValue.Bar
            };

            return new OkObjectResult(response);
        }
    }
}
