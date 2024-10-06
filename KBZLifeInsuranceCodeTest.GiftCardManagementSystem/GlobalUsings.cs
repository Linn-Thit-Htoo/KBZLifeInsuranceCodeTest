﻿global using System.Text;
global using KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;
global using KBZLifeInsuranceCodeTest.DTOs.Features.Account;
global using KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Dependencies;
global using KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account;
global using KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.CreateAccount;
global using KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.Login;
global using KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard;
global using KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.UpdateGiftCard;
global using KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Middleware;
global using KBZLifeInsuranceCodeTest.Shared;
global using KBZLifeInsuranceCodeTest.Shared.Services;
global using KBZLifeInsuranceCodeTest.Shared.Services.AuthServices;
global using KBZLifeInsuranceCodeTest.Shared.Services.CacheServices;
global using KBZLifeInsuranceCodeTest.Shared.Services.QRServices;
global using KBZLifeInsuranceCodeTest.Shared.Services.SecurityServices;
global using KBZLifeInsuranceCodeTest.Utils;
global using MediatR;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using KBZLifeInsuranceCodeTest.Utils.Enums;
global using Microsoft.AspNetCore.Mvc;
