using KBZLifeInsuranceCodeTest.DTOs.Features.GiftCard;
using KBZLifeInsuranceCodeTest.Utils;
using MediatR;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.MigrateGiftCard
{
    public class MigrateGiftCardCommandHandler : IRequestHandler<MigrateGiftCardCommand, Result<GiftCardDTO>>
    {
        private readonly IGiftCardRepository _giftCardRepository;

        public MigrateGiftCardCommandHandler(IGiftCardRepository gateCardRepository)
        {
            _giftCardRepository = gateCardRepository;
        }

        public async Task<Result<GiftCardDTO>> Handle(MigrateGiftCardCommand request, CancellationToken cancellationToken)
        {
            return await _giftCardRepository.MigrateGiftCardAsync(cancellationToken);
        }
    }
}
