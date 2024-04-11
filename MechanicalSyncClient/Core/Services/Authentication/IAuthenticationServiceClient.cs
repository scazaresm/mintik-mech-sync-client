using MechanicalSyncApp.Core.Services.Authentication;
using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.Authentication.Models.Request;
using MechanicalSyncApp.Core.Services.Authentication.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.AuthenticationService
{
    public interface IAuthenticationServiceClient
    {
        event EventHandler<RefreshAuthenticationTokenEventArgs> OnAuthenticationTokenRefresh;

        string AuthenticationToken { get; }

        UserDetails LoggedUserDetails { get; }

        Task<LoginResponse> LoginAsync(LoginRequest request);

        Task<UserDetails> GetUserDetailsAsync(string userId);

        Task<List<UserDetails>> GetAllUserDetailsAsync();

        Task<UserDetails> RegisterUserAsync(RegisterUserRequest request);

        Task ResetPasswordAsync(string userId);

        Task ChangeInitialPasswordAsync(string newPassword);
        Task<UserDetails> UpdateUser(string userId, UpdateUserRequest request);
    }
}
