using KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;
using KBZLifeInsuranceCodeTest.DTOs.Features.GiftCard;
using KBZLifeInsuranceCodeTest.DTOs.Features.PageSetting;
using KBZLifeInsuranceCodeTest.Extensions;
using KBZLifeInsuranceCodeTest.Shared;
using KBZLifeInsuranceCodeTest.Shared.Services.CacheServices;
using KBZLifeInsuranceCodeTest.Shared.Services.QRServices;
using KBZLifeInsuranceCodeTest.Utils;
using KBZLifeInsuranceCodeTest.Utils.Enums;
using Microsoft.EntityFrameworkCore;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard
{
    public class GiftCardRepository : IGiftCardRepository
    {
        private readonly AppDbContext _context;
        private readonly QRService _qrService;
        private readonly string _cacheKey;

        public GiftCardRepository(AppDbContext context, QRService qrService)
        {
            _context = context;
            _qrService = qrService;
        }

        public async Task<Result<GiftCardListDTO>> GetGiftCardListAsync(int pageNo, int pageSize, CancellationToken cs)
        {
            Result<GiftCardListDTO> result;
            try
            {
                var query = _context.TblGiftcards.OrderByDescending(x => x.GiftCardId).Where(x => !x.IsDeleted);

                var lst = await query.Paginate(pageNo, pageSize).ToListAsync(cancellationToken: cs);
                var totalCount = await query.CountAsync(cancellationToken: cs);
                var pageCount = totalCount / pageSize;

                if (totalCount % pageSize > 0)
                {
                    pageCount++;
                }

                var pageSetting = new PageSettingDTO(pageNo, pageSize, pageCount, totalCount);
                var model = new GiftCardListDTO(lst.Select(x => x.ToDto()).ToList(), pageSetting);
                result = Result<GiftCardListDTO>.Success(model);
            }
            catch (Exception ex)
            {
                result = Result<GiftCardListDTO>.Fail(ex);
            }

        result:
            return result;
        }

        public async Task<Result<GiftCardDTO>> GetGiftCardByIdAsync(string id, CancellationToken cs)
        {
            Result<GiftCardDTO> result;
            try
            {
                var item = await _context.TblGiftcards.FirstOrDefaultAsync(x => x.GiftCardId == id && !x.IsDeleted, cancellationToken: cs);
                if (item is null)
                {
                    result = Result<GiftCardDTO>.NotFound();
                    goto result;
                }

                result = Result<GiftCardDTO>.Success(item.ToDto());
            }
            catch (Exception ex)
            {
                result = Result<GiftCardDTO>.Fail(ex);
            }

        result:
            return result;
        }

        public async Task<Result<GiftCardDTO>> GetGiftCardByCodeAsync(string giftCartNo, CancellationToken cs)
        {
            Result<GiftCardDTO> result;
            try
            {
                var item = await _context.TblGiftcards.FirstOrDefaultAsync(x => x.GiftCardNo == giftCartNo && !x.IsDeleted, cancellationToken: cs);
                if (item is null)
                {
                    result = Result<GiftCardDTO>.NotFound();
                    goto result;
                }

                result = Result<GiftCardDTO>.Success(item.ToDto());
            }
            catch (Exception ex)
            {
                result = Result<GiftCardDTO>.Fail(ex);
            }

        result:
            return result;
        }

        public async Task<Result<GiftCardDTO>> MigrateGiftCardAsync(CancellationToken cs)
        {
            Result<GiftCardDTO> result;
            try
            {
                for (int i = 1; i <= 1000; i++)
                {
                    var promoCode = DevCode.GeneratePromoCode();
                    var qr = _qrService.GenerateQRCodeByte(promoCode);
                    var model = new TblGiftcard()
                    {
                        GiftCardId = Ulid.NewUlid().ToString(),
                        Title = $"Gift Card - {i}",
                        Description = "Special promo card, redeemable at all partner stores",
                        CreatedDate = DateTime.Now,
                        GiftCardDuration = 6,
                        GiftCardNo = promoCode,
                        IsDeleted = false,
                        Qrcode = qr,
                        Status = Convert.ToString(EnumGiftCardStatus.Unused)!,
                        Amount = 100000
                    };

                    bool isDuplicate = await _context.TblGiftcards.AnyAsync(x => x.GiftCardNo == promoCode && !x.IsDeleted, cancellationToken: cs);
                    if (isDuplicate)
                    {
                        result = Result<GiftCardDTO>.Duplicate("Promo Code duplicate.");
                        goto result;
                    }

                    await _context.TblGiftcards.AddAsync(model, cs);
                }

                await _context.SaveChangesAsync(cs);
                result = Result<GiftCardDTO>.Success();
            }
            catch (Exception ex)
            {
                result = Result<GiftCardDTO>.Fail(ex);
            }

        result:
            return result;
        }

        public async Task<Result<GiftCardDTO>> UpdateGiftCardAsync(GiftCardRequestDTO giftCardRequest, string id, CancellationToken cs)
        {
            Result<GiftCardDTO> result;
            try
            {
                var item = await _context.TblGiftcards.FirstOrDefaultAsync(x => x.GiftCardId == id && !x.IsDeleted, cancellationToken: cs);
                if (item is null)
                {
                    result = Result<GiftCardDTO>.NotFound();
                    goto result;
                }

                item.CashbackPercentage = giftCardRequest.CashbackPercentage;
                item.PhoneNumber = giftCardRequest.PhoneNumber;
                item.UpdatedDate = DateTime.Now;
                _context.TblGiftcards.Update(item);

                await _context.SaveChangesAsync(cs);
                result = Result<GiftCardDTO>.Success();
            }
            catch (Exception ex)
            {
                result = Result<GiftCardDTO>.Fail(ex);
            }

        result:
            return result;
        }

        public async Task<Result<GiftCardDTO>> DeactivateGiftCardAsync(string id, CancellationToken cs)
        {
            Result<GiftCardDTO> result;
            try
            {
                var item = await _context.TblGiftcards.FindAsync([id], cancellationToken: cs);
                if (item is null)
                {
                    result = Result<GiftCardDTO>.NotFound();
                    goto result;
                }

                item.IsDeleted = true;
                _context.TblGiftcards.Update(item);

                await _context.SaveChangesAsync(cs);
                result = Result<GiftCardDTO>.Success();
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
