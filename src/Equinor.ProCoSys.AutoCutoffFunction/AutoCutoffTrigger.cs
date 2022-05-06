using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Equinor.ProCoSys.AutoCutoffFunction;

public class AutoCutoffTrigger
{
    private ILogger logger;
    private const string Schedule1 = "0 0 1 * * Mon"; // Each Monday night at 01:00
    private const string Schedule2 = "*/10 * * * * *"; // Each 10'th second

    [FunctionName("AutoCutoffTrigger")]
    public void Run([TimerTrigger(Schedule2)]TimerInfo timerInfo, ILogger log)
    {
        logger = log;
        logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
    }
}
