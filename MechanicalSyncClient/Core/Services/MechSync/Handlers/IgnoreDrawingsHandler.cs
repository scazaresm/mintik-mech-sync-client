using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Util;
using Newtonsoft.Json;
using Polly;
using Serilog;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.Core.Services.MechSync.Handlers
{
    public class IgnoreDrawingsHandler
    {
        private readonly HttpClient client;
        private readonly IgnoreDrawingsRequest request;
        private readonly ILogger logger;

        public IgnoreDrawingsHandler(
              HttpClient client,
              IgnoreDrawingsRequest request,
              ILogger logger
          )
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.request = request ?? throw new ArgumentNullException(nameof(request));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Version> HandleAsync()
        {
            return await Policy
            .Handle<TaskCanceledException>()
            .Or<HttpRequestException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(async () =>
            {
                string jsonRequest = JsonUtils.SerializeWithCamelCase(request);

                HttpResponseMessage response = await client.PutAsync(
                    "versions/ignore-drawings",
                    new StringContent(jsonRequest, Encoding.UTF8, "application/json")
                );
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

                    throw new HttpRequestException(
                        $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                    );
                }
                return JsonConvert.DeserializeObject<Version>(responseContent);
            });
        }
    }
}
