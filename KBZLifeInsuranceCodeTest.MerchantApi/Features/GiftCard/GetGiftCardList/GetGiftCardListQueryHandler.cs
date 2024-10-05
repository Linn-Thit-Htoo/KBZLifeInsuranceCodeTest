using KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;
using KBZLifeInsuranceCodeTest.DTOs.Features.GiftCard;
using KBZLifeInsuranceCodeTest.MerchantApi.Features.GiftCard;
using KBZLifeInsuranceCodeTest.Utils;
using KBZLifeInsuranceCodeTest.Utils.Resources;
using MediatR;

namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.GiftCard.GetGiftCardList
{
    public class GetGiftCardListQueryHandler : IRequestHandler<GetGiftCardListQuery, Result<GiftCardListDTO>>
    {
        private readonly IGiftCardRepository _giftCardRepository;

        public GetGiftCardListQueryHandler(IGiftCardRepository giftCardRepository)
        {
            _giftCardRepository = giftCardRepository;
        }

        public async Task<Result<GiftCardListDTO>> Handle(GetGiftCardListQuery request, CancellationToken cancellationToken)
        {
            Result<GiftCardListDTO> result;
            try
            {
                if (request.PageNo <= 0)
                {
                    result = Result<GiftCardListDTO>.Fail(MessageResource.InvalidPageNo);
                    goto result;
                }

                if (request.PageSize <= 0)
                {
                    result = Result<GiftCardListDTO>.Fail(MessageResource.InvalidPageSize);
                    goto result;
                }

                result = await _giftCardRepository.GetGiftCardListAsync(request.PageNo, request.PageSize, cancellationToken);
            }
            catch (Exception ex)
            {
                result = Result<GiftCardListDTO>.Fail(ex);
            }

        result:
            return result;
        }
    }
}
