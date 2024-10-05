namespace KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoice;

public class PurchaseInvoiceDTO
{
    public PurchaseInvoiceDataModel PurchaseInvoice { get; set; }
    public List<PurchaseInvoiceDetailDataModel> PurchaseInvoiceDetails { get; set; }
}
