using KBZLifeInsuranceCodeTest.DTOs.Features.Account;
using KBZLifeInsuranceCodeTest.Shared.Services.AuthServices;
using KBZLifeInsuranceCodeTest.Utils;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account
{
    public interface IAccountRepository
    {
        Task<Result<AccountListDTO>> GetAccountListAsync(int pageNo, int pageSize, CancellationToken cs);
        Task<Result<AccountDTO>> CreateAccountAsync(AccountRequestDTO accountRequest, CancellationToken cs);
        Task<Result<JwtResponseModel>> LoginAsync(LoginRequestDTO loginRequest, CancellationToken cs);
    }
}
