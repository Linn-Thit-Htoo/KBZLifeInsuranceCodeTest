using KBZLifeInsuranceCodeTest.DTOs.Features.GiftCard;
using KBZLifeInsuranceCodeTest.Shared;
using KBZLifeInsuranceCodeTest.Utils;
using KBZLifeInsuranceCodeTest.Utils.Resources;
using MediatR;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.DeactivateGiftCard
{
    public class DeactivateGiftCardCommandHandler : IRequestHandler<DeactivateGiftCardCommand, Result<GiftCardDTO>>
    {
        private readonly IGiftCardRepository _giftCardRepository;

        public DeactivateGiftCardCommandHandler(IGiftCardRepository giftCardRepository)
        {
            _giftCardRepository = giftCardRepository;
        }

        public async Task<Result<GiftCardDTO>> Handle(DeactivateGiftCardCommand request, CancellationToken cancellationToken)
        {
            Result<GiftCardDTO> result;
            try
            {
                if (request.GiftCardId.IsNullOrEmpty())
                {
                    result = Result<GiftCardDTO>.Fail(MessageResource.InvalidId);
                    goto result;
                }

                result = await _giftCardRepository.DeactivateGiftCardAsync(request.GiftCardId, cancellationToken);
            }
            catch (Exception ex)
            {
                result = Result<GiftCardDTO>.Fail(ex);
            }

        result:
            return result;
        }
    }
}
