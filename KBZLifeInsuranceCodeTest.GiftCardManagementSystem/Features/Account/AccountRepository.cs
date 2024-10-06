namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;
    private readonly AesService _aesService;

    public AccountRepository(AppDbContext context, AesService aesService)
    {
        _context = context;
        _aesService = aesService;
    }

    public async Task<Result<AccountListDTO>> GetAccountListAsync(
        int pageNo,
        int pageSize,
        CancellationToken cs
    )
    {
        Result<AccountListDTO> result;
        try
        {
            var query = _context.TblUsers.OrderByDescending(x => x.UserId).Where(x => !x.IsDeleted);

            var lst = await query.Paginate(pageNo, pageSize).ToListAsync(cancellationToken: cs);
            var totalCount = await query.CountAsync(cancellationToken: cs);
            var pageCount = totalCount / pageSize;

            if (totalCount % pageSize > 0)
            {
                pageCount++;
            }

            var pageSetting = new PageSettingDTO(pageNo, pageSize, pageCount, totalCount);
            var model = new AccountListDTO(lst.Select(x => x.ToDto()).ToList(), pageSetting);

            result = Result<AccountListDTO>.Success(model);
        }
        catch (Exception ex)
        {
            result = Result<AccountListDTO>.Fail(ex);
        }

    result:
        return result;
    }

    public async Task<Result<AccountDTO>> CreateAccountAsync(
        AccountRequestDTO accountRequest,
        CancellationToken cs
    )
    {
        Result<AccountDTO> result;
        try
        {
            bool phoneNoDuplicate = await _context.TblUsers.AnyAsync(
                x => x.PhoneNumber == accountRequest.PhoneNumber && !x.IsDeleted,
                cancellationToken: cs
            );
            if (phoneNoDuplicate)
            {
                result = Result<AccountDTO>.Duplicate("Phone Number already exists.");
                goto result;
            }

            await _context.TblUsers.AddAsync(accountRequest.ToEntity(), cs);
            await _context.SaveChangesAsync(cs);

            result = Result<AccountDTO>.Success();
        }
        catch (Exception ex)
        {
            result = Result<AccountDTO>.Fail(ex);
        }

    result:
        return result;
    }

    public async Task<Result<JwtResponseModel>> LoginAsync(
        LoginRequestDTO loginRequest,
        CancellationToken cs
    )
    {
        Result<JwtResponseModel> result;
        try
        {
            var user = await _context.TblUsers.FirstOrDefaultAsync(
                x => x.PhoneNumber == loginRequest.PhoneNumber && !x.IsDeleted,
                cancellationToken: cs
            );

            if (user is null)
            {
                result = Result<JwtResponseModel>.NotFound("Login Fail.");
                goto result;
            }

            var decryptedDbPassword = _aesService.Decrypt(user.Password);
            var decryptedRequestPassword = _aesService.Decrypt(loginRequest.Password);

            if (!decryptedDbPassword.Equals(decryptedRequestPassword))
            {
                result = Result<JwtResponseModel>.Fail("Password is incorrect.");
                goto result;
            }

            var jwtModel = GetJwtResponseModel(user);
            result = Result<JwtResponseModel>.Success(jwtModel);
        }
        catch (Exception ex)
        {
            result = Result<JwtResponseModel>.Fail(ex);
        }

    result:
        return result;
    }

    private JwtResponseModel GetJwtResponseModel(TblUser tblUser)
    {
        return new JwtResponseModel(
            _aesService.Encrypt(tblUser.UserId),
            _aesService.Encrypt(tblUser.UserName),
            _aesService.Encrypt(tblUser.UserRole)
        );
    }
}
