using KBZLifeInsuranceCodeTest.DTOs.Features.GiftCard;
using KBZLifeInsuranceCodeTest.Utils;

namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.GiftCard.GetGiftCardByCode;

public class GetGiftCardByCodeQuery : IRequest<Result<GiftCardDTO>>
{
    public string GiftCardCode { get; set; }

    public GetGiftCardByCodeQuery(string giftCardCode)
    {
        GiftCardCode = giftCardCode;
    }
}
