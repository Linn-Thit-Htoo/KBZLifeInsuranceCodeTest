﻿using KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;
using KBZLifeInsuranceCodeTest.DTOs.Features.GiftCard;
using KBZLifeInsuranceCodeTest.DTOs.Features.PageSetting;
using KBZLifeInsuranceCodeTest.Extensions;
using KBZLifeInsuranceCodeTest.Shared;
using KBZLifeInsuranceCodeTest.Utils;
using Microsoft.EntityFrameworkCore;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard
{
    public class GiftCardRepository : IGiftCardRepository
    {
        private readonly AppDbContext _context;

        public GiftCardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<GiftCardListDTO>> GetGiftCardListAsync(int pageNo, int pageSize, CancellationToken cs)
        {
            Result<GiftCardListDTO> result;
            try
            {
                var query = _context.TblGiftcards.OrderByDescending(x => x.GiftCardId)
                    .Paginate(pageNo, pageSize);

                var lst = await query.ToListAsync(cancellationToken: cs);
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
                var item = await _context.TblGiftcards.FindAsync([id], cancellationToken: cs);
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
                var item = await _context.TblGiftcards.FirstOrDefaultAsync(x => x.GiftCardNo == giftCartNo, cancellationToken: cs);
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

        public Task<Result<GiftCardDTO>> MigrateGiftCardsAsync(CancellationToken cs)
        {
            Result<GiftCardDTO> result;
            try
            {

            }
            catch (Exception ex)
            {

            }
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
