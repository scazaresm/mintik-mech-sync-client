using MechanicalSyncApp.Core.Services.Authentication.Models.Request;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Services.MechSync.Models;
using MechanicalSyncApp.Core.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MechanicalSyncApp.Core.Services.Authentication.Models;
using Polly;

namespace MechanicalSyncApp.Core.Services.Authentication.Handlers
{
    public class UpdateUserHandler
    {
        private readonly HttpClient client;
        private readonly string userId;
        private readonly UpdateUserRequest request;

        public UpdateUserHandler(HttpClient client, string userId, UpdateUserRequest request)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.userId = userId ?? throw new ArgumentNullException(nameof(userId));
            this.request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task<UserDetails> HandleAsync()
        {
            return await Policy
               .Handle<TaskCanceledException>()
               .Or<HttpRequestException>()
               .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
               .ExecuteAsync(async () =>
               {
                   try
                   {
                       string endpointUrl = $"update-user?userId={userId}";

                       string jsonRequest = JsonUtils.SerializeWithCamelCase(request);

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
                       return JsonConvert.DeserializeObject<UserDetails>(responseContent);
                   }
                   catch (Exception ex)
                   {
                       throw new Exception($"Failed to update change request: ${ex.Message}", ex);
                   }
               });
        }
    }
}
