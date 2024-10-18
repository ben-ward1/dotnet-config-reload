using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConfigReload.Lib
{
    public static class IServiceCollectionExtensions
    {
        public static void ConfigureAppOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ConfigReloadOptions>(config.GetSection("ConfigReloadSettings"));
        }
    }
}
