﻿using KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;
using KBZLifeInsuranceCodeTest.DTOs.Features.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBZLifeInsuranceCodeTest.Extensions
{
    public static class Extension
    {
        public static AccountDTO ToDto(this TblUser tblUser)
        {
            return new AccountDTO
            {
                UserId = tblUser.UserId,
                UserName = tblUser.UserName,
                PhoneNumber = tblUser.PhoneNumber,
                UserRole = tblUser.UserRole,
                IsDeleted = tblUser.IsDeleted
            };
        }

        public static TblUser ToEntity(this AccountRequestDTO accountRequest)
        {
            return new TblUser
            {
                UserId = Ulid.NewUlid().ToString(),
                UserName = accountRequest.UserName,
                PhoneNumber = accountRequest.PhoneNumber,
                UserRole = accountRequest.UserRole,
                Password = accountRequest.Password,
                IsDeleted = false
            };
        }
    }
}
