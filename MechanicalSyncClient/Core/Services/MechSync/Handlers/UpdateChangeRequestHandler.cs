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
    public class UpdateChangeRequestHandler
    {
        private readonly HttpClient client;
        private readonly string changeRequestId;
        private readonly ChangeRequestUpdateableFields updateableFields;

        public UpdateChangeRequestHandler(
                HttpClient client, 
                string changeRequestId, 
                ChangeRequestUpdateableFields updateableFields
            )
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.changeRequestId = changeRequestId ?? throw new ArgumentNullException(nameof(changeRequestId));
            this.updateableFields = updateableFields ?? throw new ArgumentNullException(nameof(updateableFields));
        }

        public async Task<ChangeRequest> HandleAsync()
        {
            return await Policy
               .Handle<TaskCanceledException>()
               .Or<HttpRequestException>()
               .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
               .ExecuteAsync(async () =>
               {
                   try
                   {
                       string endpointUrl = $"versions/reviews/targets/change-requests?changeRequestId={changeRequestId}";

                       string jsonRequest = JsonUtils.SerializeWithCamelCase(updateableFields);

                       HttpResponseMessage response = await client.PutAsync(
                           endpointUrl,
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
                   }
                   catch (Exception ex)
                   {
                       throw new Exception($"Failed to update change request: ${ex.Message}", ex);
                   }
               });
        }
    }
}
