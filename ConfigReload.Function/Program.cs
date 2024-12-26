using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ConfigReload.Lib;
using Microsoft.Extensions.Configuration;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());

        if (hostingContext.HostingEnvironment.IsDevelopment())
        {
            var root = hostingContext.HostingEnvironment.ContentRootPath;
            
            // use file in source directory instead of bin since function app monitors published file instead of source file
            var path = Path.Combine(root, @"..\..\..\", "local.settings.json");

            config.AddJsonFile(path, optional: false, reloadOnChange: true);
        }

        config.AddEnvironmentVariables();
    })
    .ConfigureServices((hostingContext, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.ConfigureAppOptions(hostingContext.Configuration);
    })
    .Build();

host.Run();