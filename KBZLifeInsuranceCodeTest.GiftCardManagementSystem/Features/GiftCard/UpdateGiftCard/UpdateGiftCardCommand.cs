namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.UpdateGiftCard
{
    public class UpdateGiftCardCommand : IRequest<Result<GiftCardDTO>>
    {
        public string GiftCardId { get; set; }
        public GiftCardRequestDTO GiftCardRequest { get; set; }

        public UpdateGiftCardCommand(string giftCardId, GiftCardRequestDTO giftCardRequest)
        {
            GiftCardId = giftCardId;
            GiftCardRequest = giftCardRequest;
        }
    }
}
