namespace MechanicalSyncApp.Core.Services.Authentication.Models
{
    public class UserDetails
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string FullName { get; set; }
    }
}
