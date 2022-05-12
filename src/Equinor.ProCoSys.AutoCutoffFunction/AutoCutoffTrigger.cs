using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Equinor.ProCoSys.AutoCutoffFunction
{
    public class AutoCutoffTrigger
    {
        private readonly IConfiguration _configuration;
        private ILogger logger;

        public AutoCutoffTrigger(IConfiguration configuration) => _configuration = configuration;

        [FunctionName("AutoCutoffTrigger")]
        public async Task RunAsync([TimerTrigger("%Schedule%")] TimerInfo timerInfo, ILogger log)
        {
            logger = log;
            logger.LogInformation($"Starting AutoCutoffTrigger trigger at: {DateTime.Now}");

            var pcsUrl = _configuration.GetValue<string>("PCSUrl");
            logger.LogInformation($"PCSUrl: {pcsUrl}");

            var mainSecret = _configuration.GetValue<string>("MainSecret");
            logger.LogInformation($"MainSecret: {mainSecret.Substring(0, 3)}xxxxxx");

            var url = $"{pcsUrl.TrimEnd('/')}/runjob/RunAllCutoffAnonymous?key={mainSecret}";
            //var result = await AutoCutoffRunner.RunAsync(url);

            //if (result != HttpStatusCode.NoContent)
            //{
            //    throw new Exception($"AutoCutoffTrigger trigger didn't exit with expected code {HttpStatusCode.NoContent}. Got code {result}");
            //}

            logger.LogInformation($"Finished AutoCutoffTrigger trigger at: {DateTime.Now}.");
        }
    }
}
