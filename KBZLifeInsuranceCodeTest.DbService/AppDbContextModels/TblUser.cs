using System;
using System.Collections.Generic;

namespace KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;

public partial class TblUser
{
    public string UserId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string UserRole { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual ICollection<TblPurchaseInvoice> TblPurchaseInvoices { get; set; } = new List<TblPurchaseInvoice>();
}
