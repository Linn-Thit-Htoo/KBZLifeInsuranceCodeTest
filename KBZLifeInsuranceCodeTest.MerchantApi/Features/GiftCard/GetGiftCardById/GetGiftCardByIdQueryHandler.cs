namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.GiftCard.GetGiftCardById;

public class GetGiftCardByIdQueryHandler
    : IRequestHandler<GetGiftCardByIdQuery, Result<GiftCardDTO>>
{
    private readonly IGiftCardRepository _giftCardRepository;

    public GetGiftCardByIdQueryHandler(IGiftCardRepository giftCardRepository)
    {
        _giftCardRepository = giftCardRepository;
    }

    public async Task<Result<GiftCardDTO>> Handle(
        GetGiftCardByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        Result<GiftCardDTO> result;
        try
        {
            if (request.GiftCardId.IsNullOrEmpty())
            {
                result = Result<GiftCardDTO>.Fail(MessageResource.InvalidId);
                goto result;
            }

            result = await _giftCardRepository.GetGiftCardByIdAsync(
                request.GiftCardId,
                cancellationToken
            );
        }
        catch (Exception ex)
        {
            result = Result<GiftCardDTO>.Fail(ex);
        }

    result:
        return result;
    }
}
