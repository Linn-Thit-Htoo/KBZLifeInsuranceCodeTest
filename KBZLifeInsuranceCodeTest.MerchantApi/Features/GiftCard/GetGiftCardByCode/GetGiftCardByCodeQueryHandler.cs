using KBZLifeInsuranceCodeTest.Shared;

namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.GiftCard.GetGiftCardByCode
{
    public class GetGiftCardByCodeQueryHandler : IRequestHandler<GetGiftCardByCodeQuery, Result<GiftCardDTO>>
    {
        private readonly IGiftCardRepository _giftCardRepository;

        public GetGiftCardByCodeQueryHandler(IGiftCardRepository giftCardRepository)
        {
            _giftCardRepository = giftCardRepository;
        }

        public async Task<Result<GiftCardDTO>> Handle(GetGiftCardByCodeQuery request, CancellationToken cancellationToken)
        {
            Result<GiftCardDTO> result;
            try
            {
                if (request.GiftCardCode.IsNullOrEmpty())
                {
                    result = Result<GiftCardDTO>.Fail("Invalid Code.");
                    goto result;
                }

                result = await _giftCardRepository.GetGiftCardByCodeAsync(request.GiftCardCode, cancellationToken);
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
