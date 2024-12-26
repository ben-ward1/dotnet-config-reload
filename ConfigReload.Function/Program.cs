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

            var sourceSettingsPath = Path.Combine(root, @"..\..\..\", "custom.source.json"); // will reload on change
            var publishSettingsPath = "custom.publish.json"; // will NOT reload on change

            config.AddJsonFile(sourceSettingsPath, optional: false, reloadOnChange: true);
            config.AddJsonFile(publishSettingsPath, optional: false, reloadOnChange: true);
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