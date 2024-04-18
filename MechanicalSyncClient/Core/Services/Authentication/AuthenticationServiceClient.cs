using MechanicalSyncApp.Core.AuthenticationService;
using MechanicalSyncApp.Core.Services.Authentication.Handlers;
using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.Authentication.Models.Request;
using MechanicalSyncApp.Core.Services.Authentication.Models.Response;
using MechanicalSyncApp.Properties;
using MechanicalSyncApp.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Timers;

namespace MechanicalSyncApp.Core.Services.Authentication
{
    public class RefreshAuthenticationTokenEventArgs
    {
        public string OldToken { get; set; }
        public string NewToken { get; set; }
    }

    public class AuthenticationServiceClient : IAuthenticationServiceClient, IDisposable
    {
        #region Singleton
        private readonly string SERVER_URL = Settings.Default.SERVER_URL;
        private readonly int DEFAULT_TIMEOUT_SECONDS = Settings.Default.DEFAULT_TIMEOUT_SECONDS;

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
            try
            {
                restClient = new HttpClient(new VerboseHandler(new HttpClientHandler()));
                restClient.BaseAddress = new Uri($"{SERVER_URL}/api/authentication/");
                restClient.Timeout = TimeSpan.FromSeconds(DEFAULT_TIMEOUT_SECONDS);
            }
            catch(FormatException ex)
            {
                throw new Exception("Check data types for SERVER_URL (string) and DEFAULT_TIMEOUT_SECONDS (double as string) in config file.", ex);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public int TOKEN_EXPIRATION_TIMEOUT_MINUTES { get; set; } = 30;

        public UserDetails LoggedUserDetails { get; private set; }

        private readonly HttpClient restClient;
        private bool disposedValue;

        public string AuthenticationToken { get; private set; }

        public event EventHandler<RefreshAuthenticationTokenEventArgs> OnAuthenticationTokenRefresh;

        public Timer RefreshTokenTimer { get; private set; }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var loginResponse = await new LoginHandler(restClient, request).HandleAsync();

            LoggedUserDetails = loginResponse.UserDetails; 
            AuthenticationToken = loginResponse.Token;
            restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationToken);
          
            // fire up a timer to automatically refresh the token before it expires
            RefreshTokenTimer = new Timer(TOKEN_EXPIRATION_TIMEOUT_MINUTES * 60 * 1000);
            RefreshTokenTimer.Elapsed += RefreshTokenTimer_Elapsed;
            RefreshTokenTimer.Enabled = true;

            return loginResponse;
        }

        private async void RefreshTokenTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // refresh the token
            var refreshTokenResponse = await new RefreshTokenHandler(restClient).HandleAsync();

            // inform all event subscribers that the token has been refreshed
            OnAuthenticationTokenRefresh.Invoke(this, new RefreshAuthenticationTokenEventArgs()
            {
                OldToken = AuthenticationToken,
                NewToken = refreshTokenResponse.Token
            });

            // replace old token with the new one
            AuthenticationToken = refreshTokenResponse.Token;
            restClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthenticationToken);
        }

        public async Task<UserDetails> GetUserDetailsAsync(string userId)
        {
            return await new GetUserDetailsHandler(restClient, userId).HandleAsync();
        }

        public Task<List<UserDetails>> GetAllUserDetailsAsync()
        {
            return new GetAllUserDetailsHandler(restClient).HandleAsync();
        }

        public async Task<UserDetails> RegisterUserAsync(RegisterUserRequest request)
        {
            return await new RegisterUserHandler(restClient, request).HandleAsync();
        }

        public async Task ChangeInitialPasswordAsync(string newPassword)
        {
            await new ChangeInitialPasswordHandler(restClient, newPassword).HandleAsync();
        }

        public async Task ResetPasswordAsync(string userId)
        {
            await new ResetPasswordHandler(restClient, userId).HandleAsync();
        }

        public async Task<UserDetails> UpdateUser(string userId, UpdateUserRequest request)
        {
            return await new UpdateUserHandler(restClient, userId, request).HandleAsync();
        }

        #region Dispose pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    restClient.Dispose();
                }
                disposedValue = true;
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
