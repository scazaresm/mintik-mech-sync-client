﻿using MechanicalSyncApp.Core.Services.Authentication.Models.Response;
using Newtonsoft.Json;
using Polly;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.Authentication.Handlers
{
    public class RefreshTokenHandler
    {
        private readonly HttpClient _client;

        public RefreshTokenHandler(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<RefreshTokenResponse> HandleAsync()
        {
            return await Policy
                .Handle<TaskCanceledException>()
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                .ExecuteAsync(async () =>
                {
                    HttpResponseMessage response = await _client.PostAsync(
                        "refresh-token",
                        new StringContent(string.Empty)
                    );

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseJson = JsonConvert.DeserializeObject<RefreshTokenResponse>(responseContent);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException(
                            $"HTTP request failed with status code {response.StatusCode}: {responseJson.Error}"
                        );
                    }

                    return responseJson;
                });
        }

    }
}
