using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZLifeInsuranceCodeTest.DTOs.Features.Account
{
    public class LoginRequestDTO
    {
        public string PhoneNumber { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
