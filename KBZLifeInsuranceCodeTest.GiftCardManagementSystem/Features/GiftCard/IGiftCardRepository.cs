using KBZLifeInsuranceCodeTest.DTOs.Features.GiftCard;
using KBZLifeInsuranceCodeTest.Utils;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard
{
    public interface IGiftCardRepository
    {
        Task<Result<GiftCardListDTO>> GetGiftCardListAsync(int pageNo, int pageSize, CancellationToken cs);
        Task<Result<GiftCardDTO>> GetGiftCardByIdAsync(string id, CancellationToken cs);
    }
}
