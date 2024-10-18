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
            config.AddJsonFile("local.settings.json", optional: false, reloadOnChange: true);
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