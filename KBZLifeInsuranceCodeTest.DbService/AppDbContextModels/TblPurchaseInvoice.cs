namespace KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;

public partial class TblPurchaseInvoice
{
    public string PurchaseInvoiceId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string InvoiceNo { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public DateTime CreatedDate { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public virtual TblUser User { get; set; } = null!;
}
