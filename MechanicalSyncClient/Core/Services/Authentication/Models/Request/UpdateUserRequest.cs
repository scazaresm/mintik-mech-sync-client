using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Services.Authentication.Models.Request
{
    public class UpdateUserRequest
    {
        public string Id { get; set; }

        public string  FirstName { get; set; }

        public string LastName { get; set; }

        public string DisplayName { get; set; }

        public string Role { get; set; }

        public bool Enabled { get; set; }
    }
}
