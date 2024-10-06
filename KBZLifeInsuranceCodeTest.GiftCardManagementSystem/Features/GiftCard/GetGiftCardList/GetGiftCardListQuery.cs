namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.GetGiftCardList;

public class GetGiftCardListQuery : IRequest<Result<GiftCardListDTO>>
{
    public int PageNo { get; set; }
    public int PageSize { get; set; }

    public GetGiftCardListQuery(int pageNo, int pageSize)
    {
        PageNo = pageNo;
        PageSize = pageSize;
    }
}
