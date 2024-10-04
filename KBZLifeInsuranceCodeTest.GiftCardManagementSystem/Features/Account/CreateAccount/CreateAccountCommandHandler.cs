using KBZLifeInsuranceCodeTest.DTOs.Features.Account;
using KBZLifeInsuranceCodeTest.Utils;
using KBZLifeInsuranceCodeTest.Utils.Resources;
using MediatR;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Result<AccountListDTO>>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Result<AccountListDTO>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            Result<AccountListDTO> result;
            try
            {
                if (request.PageNo <= 0)
                {
                    result = Result<AccountListDTO>.Fail(MessageResource.InvalidPageNo);
                    goto result;
                }

                if (request.PageSize <= 0)
                {
                    result = Result<AccountListDTO>.Fail(MessageResource.InvalidPageSize);
                    goto result;
                }

                result = await _accountRepository.GetAccountListAsync(request.PageNo, request.PageSize, cancellationToken);
            }
            catch (Exception ex)
            {
                result = Result<AccountListDTO>.Fail(ex);
            }

        result:
            return result;
        }
    }
}
