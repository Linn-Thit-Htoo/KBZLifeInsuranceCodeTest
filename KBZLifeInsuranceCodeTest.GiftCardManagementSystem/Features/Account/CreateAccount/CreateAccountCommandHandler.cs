using KBZLifeInsuranceCodeTest.DTOs.Features.Account;
using KBZLifeInsuranceCodeTest.Shared.Services.SecurityServices;
using KBZLifeInsuranceCodeTest.Utils;
using KBZLifeInsuranceCodeTest.Utils.Enums;
using MediatR;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.CreateAccount
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Result<AccountDTO>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly CreateAccountValidator _createAccountValidator;
        private readonly AesService _aesService;

        public CreateAccountCommandHandler(IAccountRepository accountRepository, CreateAccountValidator createAccountValidator, AesService aesService)
        {
            _accountRepository = accountRepository;
            _createAccountValidator = createAccountValidator;
            _aesService = aesService;
        }

        public async Task<Result<AccountDTO>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            Result<AccountDTO> result;
            try
            {
                var validationResult = await _createAccountValidator.ValidateAsync(request.AccountRequest, cancellationToken);
                if (!validationResult.IsValid)
                {
                    string errors = string.Join(" ", validationResult.Errors.Select(x => x.ErrorMessage));
                    result = Result<AccountDTO>.Fail(errors);
                    goto result;
                }

                if (!Enum.IsDefined(typeof(EnumUserRole), request.AccountRequest.UserRole))
                {
                    result = Result<AccountDTO>.Fail("Invalid User Role");
                    goto result;
                }

                // key handshake
                _aesService.Decrypt(request.AccountRequest.Password);

                result = await _accountRepository.CreateAccountAsync(request.AccountRequest, cancellationToken);
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
