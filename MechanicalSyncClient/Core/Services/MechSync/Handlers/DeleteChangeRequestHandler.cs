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
    public class DeleteChangeRequestHandler
    {
        private readonly HttpClient client;
        private readonly string changeRequestId;

        public DeleteChangeRequestHandler(HttpClient client, string changeRequestId) 
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.changeRequestId = changeRequestId ?? throw new ArgumentNullException(nameof(changeRequestId));
        }

        public async Task HandleAsync()
        {
            await Policy
              .Handle<TaskCanceledException>()
              .Or<HttpRequestException>()
              .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
              .ExecuteAsync(async () =>
              {
                  var queryParameters = new Dictionary<string, string>
                  {
                    { "changeRequestId", changeRequestId },
                  };

                  var uri = new QueryUriGenerator("versions/reviews/targets/change-requests", queryParameters).Generate();
                  HttpResponseMessage response = await client.DeleteAsync(uri);

                  var responseContent = await response.Content.ReadAsStringAsync();

                  if (!response.IsSuccessStatusCode)
                  {
                      var errorJson = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);
                      throw new HttpRequestException(
                          $"HTTP request failed with status code {response.StatusCode}: {errorJson.Error}"
                      );
                  }
              });
        }

    }
}
