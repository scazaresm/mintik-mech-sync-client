using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Util;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.Authentication.Handlers
{
    public class ResetPasswordHandler
    {
        private readonly HttpClient client;
        private readonly string userId;

        public ResetPasswordHandler(HttpClient client, string userId)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.userId = userId ?? throw new ArgumentNullException(nameof(userId));
        }

        public async Task HandleAsync()
        {
            await Policy
                .Handle<TaskCanceledException>()
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    string jsonRequest = JsonUtils.SerializeWithCamelCase(new { userId });

                    HttpResponseMessage response = await client.PostAsync(
                        "reset-password",
                        new StringContent(jsonRequest, Encoding.UTF8, "application/json")
                    );

                    if (!response.IsSuccessStatusCode)
                    {
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.Unauthorized:
                                throw new UnauthorizedAccessException("Invalid credentials");

                            default:
                                throw new HttpRequestException(
                                    $"HTTP request failed with status code {response.StatusCode}"
                                );
                        }
                    }
                    var responseContent = await response.Content.ReadAsStringAsync();
                });
        }
    }
}
