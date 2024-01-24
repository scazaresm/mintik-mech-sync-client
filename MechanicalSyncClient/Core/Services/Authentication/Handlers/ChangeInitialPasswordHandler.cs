using MechanicalSyncApp.Core.Services.Authentication.Models.Response;
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
    public class ChangeInitialPasswordHandler
    {
        private readonly HttpClient client;
        private readonly string newPassword;

        public ChangeInitialPasswordHandler(HttpClient client, string newPassword)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.newPassword = newPassword ?? throw new ArgumentNullException(nameof(newPassword));
        }

        public async Task HandleAsync()
        {
            string jsonRequest = JsonUtils.SerializeWithCamelCase(new {newPassword});

            HttpResponseMessage response = await client.PostAsync(
                "change-initial-password",
                new StringContent(jsonRequest, Encoding.UTF8, "application/json")
            );

            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        throw new UnauthorizedAccessException("Invalid credentials");

                    default:
                        throw new HttpRequestException(
                            $"HTTP request failed with status code {response.StatusCode}"
                        ); ;
                }
            }
        } 
    }
}
