using KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;
using KBZLifeInsuranceCodeTest.DTOs.Features.Account;
using KBZLifeInsuranceCodeTest.DTOs.Features.PageSetting;
using KBZLifeInsuranceCodeTest.Extensions;
using KBZLifeInsuranceCodeTest.Shared;
using KBZLifeInsuranceCodeTest.Shared.Services.SecurityServices;
using KBZLifeInsuranceCodeTest.Utils;
using Microsoft.EntityFrameworkCore;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;
        private readonly AesService _aesService;

        public AccountRepository(AppDbContext context, AesService aesService)
        {
            _context = context;
            _aesService = aesService;
        }

        public async Task<Result<AccountListDTO>> GetAccountListAsync(int pageNo, int pageSize, CancellationToken cs)
        {
            Result<AccountListDTO> result;
            try
            {
                var query = _context.TblUsers
                    .OrderByDescending(x => x.UserId)
                    .Where(x => !x.IsDeleted)
                    .Paginate(pageNo, pageSize);

                var lst = await query.ToListAsync(cancellationToken: cs);
                var totalCount = await query.CountAsync(cancellationToken: cs);
                var pageCount = totalCount / pageSize;

                if (totalCount % pageSize > 0)
                {
                    pageCount++;
                }

                var pageSetting = new PageSettingDTO(pageNo, pageSize, pageCount, totalCount);
                var model = new AccountListDTO(
                    lst.Select(x => x.ToDto()).ToList(),
                    pageSetting
                    );

                result = Result<AccountListDTO>.Success(model);
            }
            catch (Exception ex)
            {
                result = Result<AccountListDTO>.Fail(ex);
            }

        result:
            return result;
        }

        public async Task<Result<AccountDTO>> CreateAccountAsync(AccountRequestDTO accountRequest, CancellationToken cs)
        {
            Result<AccountDTO> result;
            try
            {
                bool phoneNoDuplicate = await _context.TblUsers.AnyAsync(x => x.PhoneNumber == accountRequest.PhoneNumber && !x.IsDeleted, cancellationToken: cs);
                if (phoneNoDuplicate)
                {
                    result = Result<AccountDTO>.Duplicate("Phone Number already exists.");
                    goto result;
                }

                var entity = accountRequest.ToEntity();
                entity.Password = _aesService.Encrypt(accountRequest.Password);

                await _context.TblUsers.AddAsync(entity, cs);
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
    }
}
