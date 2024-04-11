using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Util;
using Newtonsoft.Json;
using Polly;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.MechSync.Handlers
{
    public class CreateChangeRequestHandler
    {
        private readonly HttpClient client;
        private readonly string reviewTargetId;
        private readonly string changeDescription;
        private readonly ILogger logger;

        public CreateChangeRequestHandler(
                HttpClient client, 
                string reviewTargetId,
                string changeDescription,
                ILogger logger
            )
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.reviewTargetId = reviewTargetId ?? throw new ArgumentNullException(nameof(reviewTargetId));
            this.changeDescription = changeDescription ?? throw new ArgumentNullException(nameof(changeDescription));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ChangeRequest> HandleAsync()
        {
            return await Policy
            .Handle<TaskCanceledException>()
            .Or<HttpRequestException>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(async () =>
            {
                string jsonRequest = JsonUtils.SerializeWithCamelCase(new
                {
                    reviewTargetId,
                    changeDescription,
                });

                HttpResponseMessage response = await client.PostAsync(
                    "versions/reviews/targets/change-requests",
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
                return JsonConvert.DeserializeObject<ChangeRequest>(responseContent);
            });
        }
    }
}
