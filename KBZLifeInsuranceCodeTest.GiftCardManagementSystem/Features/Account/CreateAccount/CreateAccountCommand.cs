using KBZLifeInsuranceCodeTest.DTOs.Features.Account;
using KBZLifeInsuranceCodeTest.Utils;
using MediatR;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.CreateAccount
{
    public class CreateAccountCommand : IRequest<Result<AccountDTO>>
    {
        public AccountRequestDTO AccountRequest { get; set; }

        public CreateAccountCommand(AccountRequestDTO accountRequest)
        {
            AccountRequest = accountRequest;
        }
    }
}
