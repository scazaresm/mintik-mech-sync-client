using MechanicalSyncApp.Core.Services.Authentication.Models;
using MechanicalSyncApp.Core.Services.Authentication.Models.Request;
using MechanicalSyncApp.Core.Services.Authentication.Models.Response;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.AuthenticationService
{
    public interface IAuthenticationServiceClient
    {
        UserDetails UserDetails { get; }

        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<RefreshTokenResponse> RefreshTokenAsync();
    }
}
