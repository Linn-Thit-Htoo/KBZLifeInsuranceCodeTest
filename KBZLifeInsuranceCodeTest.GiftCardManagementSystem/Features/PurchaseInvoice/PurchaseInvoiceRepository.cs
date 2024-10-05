using KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;
using KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoice;
using KBZLifeInsuranceCodeTest.Extensions;
using KBZLifeInsuranceCodeTest.Utils;
using Microsoft.EntityFrameworkCore;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.PurchaseInvoice
{
    public class PurchaseInvoiceRepository : IPurchaseInvoiceRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public PurchaseInvoiceRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<Result<PurchaseInvoiceDTO>> MakePaymentAsync(PurchaseInvoiceRequestDTO purchaseInvoiceRequest, CancellationToken cs)
        {
            Result<PurchaseInvoiceDTO> result;
            var transaction = await _context.Database.BeginTransactionAsync(cs);
            try
            {
                #region Check User Valid

                bool userValid = await _context.TblUsers.AnyAsync(x => x.UserId == purchaseInvoiceRequest
                .UserId && !x.IsDeleted, cancellationToken: cs);

                if (!userValid)
                {
                    result = Result<PurchaseInvoiceDTO>.NotFound();
                    goto result;
                }

                #endregion

                #region KBZ Pay Discount

                var paymentMethod = _configuration.GetSection("PaymentMethod").Value;
                decimal totalAmount = 0;
                if (purchaseInvoiceRequest.Equals(purchaseInvoiceRequest))
                {
                    decimal discountAmount = 0.05M;
                    totalAmount = purchaseInvoiceRequest.TotalAmount * discountAmount;
                }

                #endregion

                var purchaseEntity = purchaseInvoiceRequest.ToEntity(totalAmount);
                await _context.TblPurchaseInvoices.AddAsync(purchaseEntity, cs);

                foreach (var item in purchaseInvoiceRequest.PurchaseInvoiceDetailRequests)
                {
                    #region Check Gift Card Valid

                    var giftCard = await _context.TblGiftcards
                        .FirstOrDefaultAsync(x => x.GiftCardId == item.GiftCardId
                        && !x.IsDeleted, cancellationToken: cs);

                    if (giftCard is null)
                    {
                        result = Result<PurchaseInvoiceDTO>.NotFound("Promo Code invalid.");
                        goto result;
                    }

                    #endregion

                    #region Set Expiry Date After Buying

                    var cardDuration = giftCard.GiftCardDuration;
                    var todayDate = DateTime.Now;
                    DateTime expiryDate = todayDate.AddDays(cardDuration);

                    giftCard.ExpiryDate = expiryDate.ToString("yyyy-MM-dd");
                    _context.TblGiftcards.Update(giftCard);

                    #endregion

                    var purchaseDetailEntity = new TblPurchaseInvoiceDetail()
                    {
                        Id = Ulid.NewUlid().ToString(),
                        InvoiceNo = purchaseEntity.InvoiceNo,
                        GiftCardId = item.GiftCardId,
                        Quantity = item.Quantity,
                        SubTotal = item.Quantity * giftCard.Amount,
                        TypeOfBuying = item.TypeOfBuying,
                        RecipientName = item.RecipientName,
                        RecipientPhoneNumber = item.RecipientPhoneNumber
                    };

                    await _context.TblPurchaseInvoiceDetails.AddAsync(purchaseDetailEntity, cs);
                }

                await _context.SaveChangesAsync(cs);
                await transaction.CommitAsync(cs);
                result = Result<PurchaseInvoiceDTO>.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cs);
                result = Result<PurchaseInvoiceDTO>.Fail(ex);
            }

        result:
            return result;
        }
    }
}
