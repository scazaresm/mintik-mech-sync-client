using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Util;
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
    public class PublishVersionHandler
    {
        private readonly HttpClient client;
        private readonly PublishVersionRequest request;

        public PublishVersionHandler(HttpClient client, PublishVersionRequest request)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task<Version> HandleAsync()
        {
            string jsonRequest = JsonUtils.SerializeWithCamelCase(request);

            // Define a retry policy with exponential backoff
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .Or<TaskCanceledException>() // Handle TaskCanceledException
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

            // Execute the HTTP request with the retry policy
            HttpResponseMessage response = await retryPolicy.ExecuteAsync(() =>
                client.PostAsync("versions/publish", new StringContent(jsonRequest, Encoding.UTF8, "application/json")));

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}");
            }

            return JsonConvert.DeserializeObject<Version>(responseContent);
        }
    }
}
