namespace MechanicalSyncApp.Core.Services.Authentication.Models.Response
{
    public class LoginResponse
    {
        public string Error { get; set; }
        public string Token { get; set; }
        public UserDetails UserDetails { get; set; }
    }
}
