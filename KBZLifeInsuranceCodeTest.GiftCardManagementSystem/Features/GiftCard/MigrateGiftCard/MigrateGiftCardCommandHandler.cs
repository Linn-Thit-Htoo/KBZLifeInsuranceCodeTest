using KBZLifeInsuranceCodeTest.DTOs.Features.GiftCard;
using KBZLifeInsuranceCodeTest.Utils;
using MediatR;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.MigrateGiftCard
{
    public class MigrateGiftCardCommandHandler : IRequestHandler<MigrateGiftCardCommand, Result<GiftCardDTO>>
    {
        private readonly IGiftCardRepository _gateCardRepository;

        public MigrateGiftCardCommandHandler(IGiftCardRepository gateCardRepository)
        {
            _gateCardRepository = gateCardRepository;
        }

        public async Task<Result<GiftCardDTO>> Handle(MigrateGiftCardCommand request, CancellationToken cancellationToken)
        {
            return await _gateCardRepository.MigrateGiftCardAsync(cancellationToken);
        }
    }
}
