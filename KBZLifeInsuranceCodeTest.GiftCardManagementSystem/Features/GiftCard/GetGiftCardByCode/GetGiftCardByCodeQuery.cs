namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.GetGiftCardByCode;

public class GetGiftCardByCodeQuery : IRequest<Result<GiftCardDTO>>
{
    public string GiftCardCode { get; set; }

    public GetGiftCardByCodeQuery(string giftCardCode)
    {
        GiftCardCode = giftCardCode;
    }
}
