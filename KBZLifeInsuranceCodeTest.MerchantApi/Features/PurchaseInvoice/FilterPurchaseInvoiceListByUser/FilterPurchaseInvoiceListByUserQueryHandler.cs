namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.PurchaseInvoice.FilterPurchaseInvoiceListByUser;

public class FilterPurchaseInvoiceListByUserQueryHandler : IRequestHandler<FilterPurchaseInvoiceListByUserQuery, Result<PurchaseInvoiceListDTO>>
{
    private readonly IPurchaseInvoiceRepository _purchaseInvoiceRepository;

    public FilterPurchaseInvoiceListByUserQueryHandler(IPurchaseInvoiceRepository purchaseInvoiceRepository)
    {
        _purchaseInvoiceRepository = purchaseInvoiceRepository;
    }

    public async Task<Result<PurchaseInvoiceListDTO>> Handle(FilterPurchaseInvoiceListByUserQuery request, CancellationToken cancellationToken)
    {
        Result<PurchaseInvoiceListDTO> result;
        try
        {
            if (request.UserId.IsNullOrEmpty())
            {
                result = Result<PurchaseInvoiceListDTO>.Fail(MessageResource.InvalidId);
                goto result;
            }

            if (request.CardStatus.IsNullOrEmpty())
            {
                result = Result<PurchaseInvoiceListDTO>.Fail("Card Status is invalid.");
                goto result;
            }

            result = await _purchaseInvoiceRepository.FilterPurchaseInvoiceListByUserAsyncV1(request.UserId, request.CardStatus, cancellationToken);
        }
        catch (Exception ex)
        {
            result = Result<PurchaseInvoiceListDTO>.Fail(ex);
        }

    result:
        return result;
    }
}
