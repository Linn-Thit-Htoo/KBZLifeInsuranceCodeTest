using KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoice;
using KBZLifeInsuranceCodeTest.Utils;
using MediatR;

namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.PurchaseInvoice.Purchase
{
    public class PurchaseCommand : IRequest<Result<PurchaseInvoiceDTO>>
    {
        public PurchaseInvoiceRequestDTO PurchaseInvoiceRequest { get; set; }

        public PurchaseCommand(PurchaseInvoiceRequestDTO purchaseInvoiceRequest)
        {
            PurchaseInvoiceRequest = purchaseInvoiceRequest;
        }
    }
}
