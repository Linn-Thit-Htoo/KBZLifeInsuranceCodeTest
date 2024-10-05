using KBZLifeInsuranceCodeTest.DTOs.Features.GiftCard;
using KBZLifeInsuranceCodeTest.Utils;
using MediatR;

namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.GiftCard.GetGiftCardById
{
    public class GetGiftCardByIdQuery : IRequest<Result<GiftCardDTO>>
    {
        public string GiftCardId { get; set; }

        public GetGiftCardByIdQuery(string giftCardId)
        {
            GiftCardId = giftCardId;
        }
    }
}
