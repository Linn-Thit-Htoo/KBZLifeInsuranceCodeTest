namespace KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoiceDetail;

public class PurchaseInvoiceDetailDTO
{
    public string Id { get; set; } = null!;

    public string InvoiceNo { get; set; } = null!;

    public string GiftCardId { get; set; } = null!;

    public string GiftCardNo { get; set; } = null!;

    public string GiftCardTitle { get; set; } = null!;

    public string ExpiryDate { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public int GiftCardDuration { get; set; }

    public DateTime CreatedDate { get; set; }

    public int CashbackPercentage { get; set; }

    public int Quantity { get; set; }

    public decimal SubTotal { get; set; }

    public string TypeOfBuying { get; set; } = null!;

    public string? RecipientName { get; set; }

    public string? RecipientPhoneNumber { get; set; }

    public string QRCode { get; set; } = null!;
}
