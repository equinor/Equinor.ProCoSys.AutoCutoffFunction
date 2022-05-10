using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Equinor.ProCoSys.AutoCutoffFunction
{
    public class AutoCutoffTrigger
    {
        private readonly AutoCutoffSettings autoCutoffSettings;
        private ILogger logger;

        public AutoCutoffTrigger(IOptions<AutoCutoffSettings>  autoCutoffSettings)
            => this.autoCutoffSettings = autoCutoffSettings.Value;

        [FunctionName("AutoCutoffTrigger")]
        public async Task RunAsync([TimerTrigger("%Schedule%")] TimerInfo timerInfo, ILogger log)
        {
            logger = log;
            logger.LogInformation($"Starting AutoCutoffTrigger trigger at: {DateTime.Now}");

            string pcsUrl = autoCutoffSettings.PCSUrl;
            logger.LogInformation($"PCSUrl: {pcsUrl}");

            string mainSecret = autoCutoffSettings.MainSecret;
            logger.LogInformation($"MainSecret: {mainSecret.Substring(0, 3)}xxxxxx");

            var url = $"{pcsUrl.TrimEnd('/')}/runjob/RunAllCutoffAnonymous?key={mainSecret}";
            var result = await AutoCutoffRunner.RunAsync(url);

            if (result != HttpStatusCode.NoContent)
            {
                throw new Exception($"AutoCutoffTrigger trigger didn't exit with expected code {HttpStatusCode.NoContent}");
            }

            logger.LogInformation($"Finished AutoCutoffTrigger trigger at: {DateTime.Now}.");
        }
    }
}
