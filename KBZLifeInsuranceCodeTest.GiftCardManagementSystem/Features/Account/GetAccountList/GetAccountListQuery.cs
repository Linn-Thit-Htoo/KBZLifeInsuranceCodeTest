using KBZLifeInsuranceCodeTest.DTOs.Features.Account;
using KBZLifeInsuranceCodeTest.Utils;
using MediatR;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.CreateAccount
{
    public class GetAccountListQuery : IRequest<Result<AccountListDTO>>
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public GetAccountListQuery(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
