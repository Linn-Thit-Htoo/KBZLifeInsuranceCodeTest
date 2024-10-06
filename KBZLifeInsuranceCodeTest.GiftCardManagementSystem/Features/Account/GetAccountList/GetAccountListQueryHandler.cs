namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.GetAccountList;

public class GetAccountListQueryHandler : IRequestHandler<GetAccountListQuery, Result<AccountListDTO>>
{
    private readonly IAccountRepository _accountRepository;

    public GetAccountListQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Result<AccountListDTO>> Handle(GetAccountListQuery request, CancellationToken cancellationToken)
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
