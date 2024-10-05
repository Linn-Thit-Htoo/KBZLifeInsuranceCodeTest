using KBZLifeInsuranceCodeTest.DTOs.Features.GiftCard;
using KBZLifeInsuranceCodeTest.Shared;
using KBZLifeInsuranceCodeTest.Utils;
using KBZLifeInsuranceCodeTest.Utils.Resources;
using MediatR;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.UpdateGiftCard
{
    public class UpdateGiftCardCommandHandler : IRequestHandler<UpdateGiftCardCommand, Result<GiftCardDTO>>
    {
        private readonly IGiftCardRepository _giftCardRepository;
        private readonly UpdateGIftCardValidator _updateGiftCardValidator;

        public UpdateGiftCardCommandHandler(IGiftCardRepository giftCardRepository, UpdateGIftCardValidator updateGiftCardValidator)
        {
            _giftCardRepository = giftCardRepository;
            _updateGiftCardValidator = updateGiftCardValidator;
        }

        public async Task<Result<GiftCardDTO>> Handle(UpdateGiftCardCommand request, CancellationToken cancellationToken)
        {
            Result<GiftCardDTO> result;
            try
            {
                var validationResult = await _updateGiftCardValidator.ValidateAsync(request.GiftCardRequest, cancellationToken);
                if (!validationResult.IsValid)
                {
                    string errors = string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage));
                    result = Result<GiftCardDTO>.Fail(errors);
                    goto result;
                }

                if (request.GiftCardId.IsNullOrEmpty())
                {
                    result = Result<GiftCardDTO>.Fail(MessageResource.InvalidId);
                    goto result;
                }

                result = await _giftCardRepository.UpdateGiftCardAsync(request.GiftCardRequest, request.GiftCardId cancellationToken);
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
