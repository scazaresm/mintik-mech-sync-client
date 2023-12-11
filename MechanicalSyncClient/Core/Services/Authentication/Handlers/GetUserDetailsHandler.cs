using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.MechSync.Models.Response;
using MechanicalSyncApp.Core.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.Authentication.Handlers
{
    public class GetUserDetailsHandler
    {
        private readonly HttpClient client;
        private readonly string userId;

        public GetUserDetailsHandler(HttpClient client, string userId) 
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.userId = userId ?? throw new ArgumentNullException(nameof(userId));
        }

        public async Task<UserDetails> HandleAsync()
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "userId", userId },
            };

            var uri = new QueryUriGenerator("user-details", queryParameters).Generate();

            HttpResponseMessage response = await client.GetAsync(uri);
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

    }
}
