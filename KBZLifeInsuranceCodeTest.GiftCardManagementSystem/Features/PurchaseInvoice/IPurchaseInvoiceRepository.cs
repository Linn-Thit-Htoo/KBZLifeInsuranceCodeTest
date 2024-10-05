using KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoice;
using KBZLifeInsuranceCodeTest.Utils;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.PurchaseInvoice
{
    public interface IPurchaseInvoiceRepository
    {
        Task<Result<PurchaseInvoiceListDTO>> FilterPurchaseInvoiceListByUserAsync(string userId, string cardStatus, CancellationToken cs);
        Task<Result<PurchaseInvoiceDTO>> MakePaymentAsync(PurchaseInvoiceRequestDTO purchaseInvoiceRequest, CancellationToken cs);
    }
}
