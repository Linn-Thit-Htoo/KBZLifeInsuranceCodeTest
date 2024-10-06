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
