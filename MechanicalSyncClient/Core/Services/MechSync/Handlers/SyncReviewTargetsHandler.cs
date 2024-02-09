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
    public class SyncReviewTargetsHandler
    {
        private readonly HttpClient client;
        private readonly string reviewId;

        public SyncReviewTargetsHandler(HttpClient client, string reviewId)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.reviewId = reviewId ?? throw new ArgumentNullException(nameof(reviewId));
        }

        public async Task<Review> HandleAsync()
        {
            return await Policy
                .Handle<TaskCanceledException>()
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    string jsonRequest = JsonUtils.SerializeWithCamelCase(new { reviewId });

                    HttpResponseMessage response = await client.PostAsync(
                        "versions/reviews/sync-targets",
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
                    return JsonConvert.DeserializeObject<Review>(responseContent);
                });
        }

    }
}
