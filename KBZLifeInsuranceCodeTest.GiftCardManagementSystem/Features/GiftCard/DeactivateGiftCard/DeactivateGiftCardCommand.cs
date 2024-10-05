
using KBZLifeInsuranceCodeTest.DTOs.Features.GiftCard;
using KBZLifeInsuranceCodeTest.Utils;
using MediatR;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.DeactivateGiftCard
{
    public class DeactivateGiftCardCommand : IRequest<Result<GiftCardDTO>>
    {
        public string GiftCardId { get; set; }

        public DeactivateGiftCardCommand(string giftCardId)
        {
            GiftCardId = giftCardId;
        }
    }
}
