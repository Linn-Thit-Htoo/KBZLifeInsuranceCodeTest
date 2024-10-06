namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.PurchaseInvoice
{
    public interface IPurchaseInvoiceRepository
    {
        Task<Result<PurchaseInvoiceListDTO>> FilterPurchaseInvoiceListByUserAsync(
            string userId,
            string cardStatus,
            CancellationToken cs
        );
        Task<Result<PurchaseInvoiceListDTO>> FilterPurchaseInvoiceListByUserAsyncV1(
            string userId,
            string cardStatus,
            CancellationToken cs
        );
        Task<Result<PurchaseInvoiceDTO>> MakePaymentAsync(
            PurchaseInvoiceRequestDTO purchaseInvoiceRequest,
            CancellationToken cs
        );
    }
}
