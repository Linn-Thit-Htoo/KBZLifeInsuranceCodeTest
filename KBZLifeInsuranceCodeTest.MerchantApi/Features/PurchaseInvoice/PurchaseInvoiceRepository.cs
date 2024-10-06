using KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoiceDetail;
using KBZLifeInsuranceCodeTest.Utils.Enums;
using System.Data;

namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.PurchaseInvoice;

public class PurchaseInvoiceRepository : IPurchaseInvoiceRepository
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly DapperService _dapperService;
    private readonly RedisService _redisService;

    public PurchaseInvoiceRepository(AppDbContext context, IConfiguration configuration, DapperService dapperService, RedisService redisService)
    {
        _context = context;
        _configuration = configuration;
        _dapperService = dapperService;
        _redisService = redisService;
    }

    public async Task<Result<PurchaseInvoiceListDTO>> FilterPurchaseInvoiceListByUserAsync(string userId, string cardStatus, CancellationToken cs)
    {
        Result<PurchaseInvoiceListDTO> result;
        try
        {
            bool userExists = await _context.TblUsers.AnyAsync(x => x.UserId == userId && !x.IsDeleted, cancellationToken: cs);
            if (!userExists)
            {
                result = Result<PurchaseInvoiceListDTO>.NotFound("User not found.");
                goto result;
            }

            List<PurchaseInvoiceDTO> purchaseInvoiceDTOs = new();
            var invoiceLstByUser = await _dapperService.QueryAsync<PurchaseInvoiceDataModel>(CommonQuery.Sp_FilterPurchaseInvoiceByUserId, new { UserId = userId }, CommandType.StoredProcedure);
            foreach (var invoice in invoiceLstByUser)
            {
                var parameters = new
                {
                    invoice.InvoiceNo,
                    Status = cardStatus
                };
                var invoiceDetailLst = await _dapperService.QueryAsync<PurchaseInvoiceDetailDataModel>(CommonQuery.Sp_GetGiftCardDetailsByInvoiceAndStatus, parameters, CommandType.StoredProcedure);

                purchaseInvoiceDTOs.Add(new PurchaseInvoiceDTO()
                {
                    PurchaseInvoice = invoice,
                    PurchaseInvoiceDetails = invoiceDetailLst
                });
            }

            var model = new PurchaseInvoiceListDTO(purchaseInvoiceDTOs);
            result = Result<PurchaseInvoiceListDTO>.Success(model);
        }
        catch (Exception ex)
        {
            result = Result<PurchaseInvoiceListDTO>.Fail(ex);
        }

    result:
        return result;
    }

    public async Task<Result<PurchaseInvoiceListDTO>> FilterPurchaseInvoiceListByUserAsyncV1(string userId, string cardStatus, CancellationToken cs)
    {
        Result<PurchaseInvoiceListDTO> result;
        try
        {
            string cacheKey = $"PurchaseInvoiceList_{userId}_{cardStatus}";

            var cachedInvoices = await _redisService.GetAsync<PurchaseInvoiceDTO>(cacheKey);
            if (cachedInvoices is not null && cachedInvoices.Any())
            {
                var cachedModel = new PurchaseInvoiceListDTO(cachedInvoices);
                result = Result<PurchaseInvoiceListDTO>.Success(cachedModel);
                goto result;
            }

            bool userExists = await _context.TblUsers.AnyAsync(x => x.UserId == userId && !x.IsDeleted, cancellationToken: cs);
            if (!userExists)
            {
                result = Result<PurchaseInvoiceListDTO>.NotFound("User not found.");
                goto result;
            }

            List<PurchaseInvoiceDTO> purchaseInvoiceDTOs = new();
            var invoiceLstByUser = await _dapperService.QueryAsync<PurchaseInvoiceDataModel>(CommonQuery.Sp_FilterPurchaseInvoiceByUserId, new { UserId = userId }, CommandType.StoredProcedure);
            foreach (var invoice in invoiceLstByUser)
            {
                var parameters = new
                {
                    invoice.InvoiceNo,
                    Status = cardStatus
                };
                var invoiceDetailLst = await _dapperService.QueryAsync<PurchaseInvoiceDetailDataModel>(CommonQuery.Sp_GetGiftCardDetailsByInvoiceAndStatus, parameters, CommandType.StoredProcedure);

                purchaseInvoiceDTOs.Add(new PurchaseInvoiceDTO()
                {
                    PurchaseInvoice = invoice,
                    PurchaseInvoiceDetails = invoiceDetailLst
                });
            }

            var model = new PurchaseInvoiceListDTO(purchaseInvoiceDTOs);
            await _redisService.SetAsync(cacheKey, purchaseInvoiceDTOs);

            result = Result<PurchaseInvoiceListDTO>.Success(model);
        }
        catch (Exception ex)
        {
            result = Result<PurchaseInvoiceListDTO>.Fail(ex);
        }

    result:
        return result;
    }

    public async Task<Result<PurchaseInvoiceDTO>> MakePaymentAsync(PurchaseInvoiceRequestDTO purchaseInvoiceRequest, CancellationToken cs)
    {
        Result<PurchaseInvoiceDTO> result;
        var transaction = await _context.Database.BeginTransactionAsync(cs);
        try
        {
            #region Check User Valid

            var user = await _context.TblUsers.FirstOrDefaultAsync(x => x.UserId == purchaseInvoiceRequest
            .UserId && !x.IsDeleted, cancellationToken: cs);

            if (user is null)
            {
                result = Result<PurchaseInvoiceDTO>.NotFound();
                goto result;
            }

            #region Check User Role Customer

            if (!EnumUserRole.Customer.ToString().Equals(user.UserRole))
            {
                result = Result<PurchaseInvoiceDTO>.NotFound("User Role is invalid.");
                goto result;
            }

            #endregion

            #endregion

            #region KBZ Pay Discount

            var paymentMethod = _configuration.GetSection("PaymentMethod").Value;
            decimal totalAmount = 0;
            if (purchaseInvoiceRequest.Equals(purchaseInvoiceRequest))
            {
                decimal discountAmount = purchaseInvoiceRequest.TotalAmount * 0.05M;
                totalAmount = purchaseInvoiceRequest.TotalAmount - discountAmount;
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
                DateTime expiryDate = todayDate.AddMonths(cardDuration);

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
