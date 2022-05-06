using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Equinor.ProCoSys.AutoCutoffFunction
{
    internal class CutoffRunner
    {
        public static async Task<HttpStatusCode> RunAsync(string url, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(30);
                var response = await client.GetAsync(url, cancellationToken);

                return response.StatusCode;
            }
        }
    }
}
