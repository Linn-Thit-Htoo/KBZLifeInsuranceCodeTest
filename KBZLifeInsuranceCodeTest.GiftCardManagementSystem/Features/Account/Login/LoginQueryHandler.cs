namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<JwtResponseModel>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly LoginValidator _loginValidator;
    private readonly JwtService _jwtService;

    public LoginQueryHandler(
        IAccountRepository accountRepository,
        LoginValidator loginValidator,
        JwtService jwtService
    )
    {
        _accountRepository = accountRepository;
        _loginValidator = loginValidator;
        _jwtService = jwtService;
    }

    public async Task<Result<JwtResponseModel>> Handle(
        LoginQuery request,
        CancellationToken cancellationToken
    )
    {
        Result<JwtResponseModel> result;
        try
        {
            var validationResult = await _loginValidator.ValidateAsync(
                request.LoginRequest,
                cancellationToken
            );
            if (!validationResult.IsValid)
            {
                string errors = string.Join(
                    " ",
                    validationResult.Errors.Select(x => x.ErrorMessage)
                );
                result = Result<JwtResponseModel>.Fail(errors);
                goto result;
            }

            result = await _accountRepository.LoginAsync(request.LoginRequest, cancellationToken);
            if (result.IsSuccess)
            {
                result.Data.Token = _jwtService.GetJwtToken(result.Data);
            }
        }
        catch (Exception ex)
        {
            result = Result<JwtResponseModel>.Fail(ex);
        }

    result:
        return result;
    }
}
