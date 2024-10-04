﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZLifeInsuranceCodeTest.DTOs.Features.Account
{
    public class AccountDTO
    {
        public string UserId { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string UserRole { get; set; } = null!;

        public ulong IsDeleted { get; set; }
    }
}
