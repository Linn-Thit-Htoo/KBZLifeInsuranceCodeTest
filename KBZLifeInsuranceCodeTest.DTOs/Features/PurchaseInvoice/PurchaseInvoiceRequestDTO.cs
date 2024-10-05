namespace KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoice
{
    public class PurchaseInvoiceRequestDTO
    {
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public List<PurchaseInvoiceDetailRequestDTO> PurchaseInvoiceDetailRequests { get; set; }
    }
}
