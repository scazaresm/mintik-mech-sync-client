using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Version = MechanicalSyncApp.Core.Services.MechSync.Models.Version;

namespace MechanicalSyncApp.Core.Services.MechSync.Handlers
{
    public class GetMyWorkHandler
    {
        private readonly HttpClient client;

        public GetMyWorkHandler(HttpClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<List<Version>> HandleAsync()
        {
            // Define a retry policy with exponential backoff
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .Or<TaskCanceledException>() // Handle TaskCanceledException
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

            // Execute the HTTP request with the retry policy
            HttpResponseMessage response = await retryPolicy.ExecuteAsync(async () =>
            {
                const string uri = "versions/ongoing/mine";
                return await client.GetAsync(uri);
            });

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}");
            }

            return JsonConvert.DeserializeObject<List<Version>>(responseContent);
        }
    }
}
