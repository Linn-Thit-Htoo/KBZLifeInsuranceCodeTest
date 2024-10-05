using KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoiceDetail;

namespace KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoice;

public class PurchaseInvoiceDTO
{
    public string PurchaseInvoiceId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string InvoiceNo { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public DateTime CreatedDate { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public List<PurchaseInvoiceDetailDTO> PurchaseInvoiceDetails { get; set; }
}
