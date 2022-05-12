using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

[assembly: FunctionsStartup(typeof(Equinor.ProCoSys.AutoCutoffFunction.Startup))]

namespace Equinor.ProCoSys.AutoCutoffFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json", false)
                .AddJsonFile("local.settings.json", true, true)
                .AddUserSecrets("76251825-95f9-4885-8a13-34f3b617721e", true)
                .Build();

            //services.AddOptions<AutoCutoffSettings>().Configure<IConfiguration>((settings, configuration) =>
            //{
            //    configuration.GetSection("AutoCutoffSettings");
            //});

            services.Configure<AutoCutoffSettings>(config.GetSection("AutoCutoffSettings"));
        }
    }
}
