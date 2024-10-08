﻿namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.GiftCard;

public interface IGiftCardRepository
{
    Task<Result<GiftCardListDTO>> GetGiftCardListAsync(
        int pageNo,
        int pageSize,
        CancellationToken cs
    );
    Task<Result<GiftCardDTO>> GetGiftCardByIdAsync(string id, CancellationToken cs);
    Task<Result<GiftCardDTO>> GetGiftCardByCodeAsync(string giftCartNo, CancellationToken cs);
}
