using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.Authentication.Handlers;
using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.Authentication.Models.Request;
using MechanicalSyncApp.Core.Services.Authentication.Models.Response;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.Authentication
{
    public class AuthenticationServiceClient : IAuthenticationServiceClient, IDisposable
    {
        #region Singleton
        private static IAuthenticationServiceClient _instance = null;

        public static IAuthenticationServiceClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AuthenticationServiceClient();
                return _instance;
            }
        }

        private AuthenticationServiceClient()
        {
            _restClient = new HttpClient();
            _restClient.BaseAddress = new Uri("http://localhost/api/authentication/");
            _restClient.Timeout = TimeSpan.FromSeconds(5);
        }
        #endregion

        public int TokenExpirationTimeoutMinutes { get; set; } = 30;
        public UserDetails UserDetails { get; private set; }

        private readonly HttpClient _restClient;
        private DateTime _authenticationTokenDatetime;
        private string _authenticationToken;
        private bool _disposedValue;

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var handler = new LoginHandler(_restClient, request);
            var response = await handler.HandleAsync();

            UserDetails = response.UserDetails;
            _authenticationToken = response.Token;
            _authenticationTokenDatetime = DateTime.Now;
            _restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authenticationToken);

            return response;
        }

        public async Task<RefreshTokenResponse> RefreshTokenAsync()
        {
            var timeSinceLastToken = DateTime.Now - _authenticationTokenDatetime;

            if (timeSinceLastToken.TotalMinutes < TokenExpirationTimeoutMinutes)
            {
                // current token should still valid, do nothing
                return new RefreshTokenResponse { Token = _authenticationToken };
            }

            // current token might be close to expire, refresh it
            var handler = new RefreshTokenHandler(_restClient);
            var response = await handler.HandleAsync();

            _authenticationToken = response.Token;
            _authenticationTokenDatetime = DateTime.Now;

            return response;
        }

        public async Task<UserDetails> GetUserDetailsAsync(string userId)
        {
            return await new GetUserDetailsHandler(_restClient, userId).HandleAsync();
        }

        public Task<List<UserDetails>> GetAllUserDetailsAsync()
        {
            return new GetAllUserDetailsHandler(_restClient).HandleAsync();
        }

        #region Dispose pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _restClient.Dispose();
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
