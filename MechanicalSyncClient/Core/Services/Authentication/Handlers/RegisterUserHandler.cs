using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.Authentication.Models.Request;
using MechanicalSyncApp.Core.Services.Authentication.Models.Response;
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
    public class RegisterUserHandler
    {
        private readonly HttpClient client;
        private readonly RegisterUserRequest request;

        public RegisterUserHandler(HttpClient client, RegisterUserRequest request)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
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
                    string jsonRequest = JsonUtils.SerializeWithCamelCase(request);

                    HttpResponseMessage response = await client.PostAsync(
                        "register",
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
                    return JsonConvert.DeserializeObject<UserDetails>(responseContent);
                });
        }

    }
}
