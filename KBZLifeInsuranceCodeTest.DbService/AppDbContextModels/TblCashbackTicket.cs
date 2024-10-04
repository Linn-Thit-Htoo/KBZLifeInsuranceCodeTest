using System;
using System.Collections.Generic;

namespace KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;

public partial class TblCashbackTicket
{
    public string Id { get; set; } = null!;

    public string? GiftCardId { get; set; }

    public int Percentage { get; set; }

    public virtual TblGiftcard? GiftCard { get; set; }
}
