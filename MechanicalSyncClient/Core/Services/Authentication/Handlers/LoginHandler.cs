using MechanicalSyncApp.Core.Services.Authentication.Models.Request;
using MechanicalSyncApp.Core.Services.Authentication.Models.Response;
using MechanicalSyncApp.Core.Util;
using Newtonsoft.Json;
using Polly;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.Authentication.Handlers
{
    public class LoginHandler
    {
        private readonly HttpClient _client;
        private readonly LoginRequest _request;

        public LoginHandler(HttpClient client, LoginRequest request)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _request = request ?? throw new ArgumentNullException(nameof(request));
        }

        public async Task<LoginResponse> HandleAsync()
        {
            string jsonRequest = JsonUtils.SerializeWithCamelCase(_request);

            // Define a retry policy with exponential backoff
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .Or<TaskCanceledException>()
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

            // Execute the HTTP request with the retry policy
            HttpResponseMessage response = await retryPolicy.ExecuteAsync(() =>
                _client.PostAsync("login", new StringContent(jsonRequest, Encoding.UTF8, "application/json"))
            );

            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new UnauthorizedAccessException("Invalid credentials");

                    default:
                        throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LoginResponse>(responseContent);
        }
    }
}
