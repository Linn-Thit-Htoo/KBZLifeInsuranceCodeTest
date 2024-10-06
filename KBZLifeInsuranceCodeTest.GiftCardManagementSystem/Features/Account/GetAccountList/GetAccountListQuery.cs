namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.GetAccountList;

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
