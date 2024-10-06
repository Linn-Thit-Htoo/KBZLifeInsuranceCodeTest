namespace KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoice;

public class PurchaseInvoiceDataModel
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string UserRole { get; set; }

    public string InvoiceNo { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedDate { get; set; }
    public string PaymentMethod { get; set; }
}
