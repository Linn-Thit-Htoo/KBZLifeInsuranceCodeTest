using KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;
using KBZLifeInsuranceCodeTest.DTOs.Features.Account;
using KBZLifeInsuranceCodeTest.DTOs.Features.PageSetting;
using KBZLifeInsuranceCodeTest.Extensions;
using KBZLifeInsuranceCodeTest.Shared;
using KBZLifeInsuranceCodeTest.Utils;
using Microsoft.EntityFrameworkCore;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
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
    }
}
