using MechanicalSyncApp.Core.Services.MechSync.Models;
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

namespace MechanicalSyncApp.Core.Services.MechSync.Handlers
{
    public class GetPublishedProjectsHandler
    {
        private readonly HttpClient client;

        public GetPublishedProjectsHandler(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<Project>> HandleAsync()
        {
            return await Policy
                .Handle<TaskCanceledException>()
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    HttpResponseMessage response = await client.GetAsync("projects/published");
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                        throw new HttpRequestException(
                            $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                        );
                    }
                    return JsonConvert.DeserializeObject<List<Project>>(responseContent);
                });
        }

    }
}
