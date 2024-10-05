﻿using KBZLifeInsuranceCodeTest.DTOs.Features.GiftCard;
using KBZLifeInsuranceCodeTest.Utils;
using MediatR;

namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.GiftCard.GetGiftCardList
{
    public class GetGiftCardListQuery : IRequest<Result<GiftCardListDTO>>
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public GetGiftCardListQuery(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
