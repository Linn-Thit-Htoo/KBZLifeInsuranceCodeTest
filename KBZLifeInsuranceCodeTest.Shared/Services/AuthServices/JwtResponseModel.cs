using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZLifeInsuranceCodeTest.Shared.Services.AuthServices
{
    public class JwtResponseModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string UserRole { get; set; }

        public string? Token { get; set; }

        public JwtResponseModel(string userId, string userName, string userRole)
        {
            UserId = userId;
            UserName = userName;
            UserRole = userRole;
        }
    }
}
