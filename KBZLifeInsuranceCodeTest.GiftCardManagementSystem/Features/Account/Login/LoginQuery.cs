using KBZLifeInsuranceCodeTest.DTOs.Features.Account;
using KBZLifeInsuranceCodeTest.Shared.Services.AuthServices;
using KBZLifeInsuranceCodeTest.Utils;
using MediatR;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.Login
{
    public class LoginQuery : IRequest<Result<JwtResponseModel>>
    {
        public LoginRequestDTO LoginRequest { get; set; }

        public LoginQuery(LoginRequestDTO loginRequest)
        {
            LoginRequest = loginRequest;
        }
    }
}
