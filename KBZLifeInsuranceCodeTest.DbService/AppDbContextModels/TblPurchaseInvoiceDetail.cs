using System;
using System.Collections.Generic;

namespace KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;

public partial class TblPurchaseInvoiceDetail
{
    public string Id { get; set; } = null!;

    public string InvoiceNo { get; set; } = null!;

    public string GiftCardId { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal SubTotal { get; set; }

    public string TypeOfBuying { get; set; } = null!;

    public string? RecipientName { get; set; }

    public string? RecipientPhoneNumber { get; set; }

    public virtual TblGiftcard GiftCard { get; set; } = null!;
}
