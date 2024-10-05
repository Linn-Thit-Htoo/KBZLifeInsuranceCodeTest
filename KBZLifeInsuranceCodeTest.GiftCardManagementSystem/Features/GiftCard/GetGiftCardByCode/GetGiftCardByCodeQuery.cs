using KBZLifeInsuranceCodeTest.DTOs.Features.GiftCard;
using KBZLifeInsuranceCodeTest.Utils;
using MediatR;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.GetGiftCardByCode
{
    public class GetGiftCardByCodeQuery : IRequest<Result<GiftCardDTO>>
    {
        public string GiftCardCode { get; set; }

        public GetGiftCardByCodeQuery(string giftCardCode)
        {
            GiftCardCode = giftCardCode;
        }
    }
}
