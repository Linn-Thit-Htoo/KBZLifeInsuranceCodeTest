using KBZLifeInsuranceCodeTest.DTOs.Features.Account;
using KBZLifeInsuranceCodeTest.Utils;
using MediatR;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Result<AccountDTO>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly CreateAccountValidator _createAccountValidator;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, CreateAccountValidator createAccountValidator)
        {
            _accountRepository = accountRepository;
            _createAccountValidator = createAccountValidator;
        }

        public Task<Result<AccountDTO>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            Result<AccountDTO> result;
            try
            {

            }
            catch (Exception ex)
            {
                result = Result<AccountDTO>.Fail(ex);
            }

        result:
            return result;
        }
    }
}
