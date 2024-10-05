namespace KBZLifeInsuranceCodeTest.Extensions;

public static class Extension
{
    public static AccountDTO ToDto(this TblUser tblUser)
    {
        return new AccountDTO
        {
            UserId = tblUser.UserId,
            UserName = tblUser.UserName,
            PhoneNumber = tblUser.PhoneNumber,
            UserRole = tblUser.UserRole,
            IsDeleted = tblUser.IsDeleted
        };
    }

    public static TblUser ToEntity(this AccountRequestDTO accountRequest)
    {
        return new TblUser
        {
            UserId = Ulid.NewUlid().ToString(),
            UserName = accountRequest.UserName,
            PhoneNumber = accountRequest.PhoneNumber,
            UserRole = accountRequest.UserRole,
            Password = accountRequest.Password,
            IsDeleted = false
        };
    }

    public static GiftCardDTO ToDto(this TblGiftcard tblGiftcard)
    {
        return new GiftCardDTO
        {
            Amount = tblGiftcard.Amount,
            CashbackPercentage = tblGiftcard.CashbackPercentage,
            CreatedDate = tblGiftcard.CreatedDate,
            Description = tblGiftcard.Description,
            ExpiryDate = tblGiftcard.ExpiryDate,
            GiftCardId = tblGiftcard.GiftCardId,
            GiftCardNo = tblGiftcard.GiftCardNo,
            IsDeleted = tblGiftcard.IsDeleted,
            PhoneNumber = tblGiftcard.PhoneNumber,
            Qrcode = tblGiftcard.Qrcode,
            Status = tblGiftcard.Status,
            Title = tblGiftcard.Title,
            UpdatedDate = tblGiftcard.UpdatedDate,
            Duration = $"{tblGiftcard.GiftCardDuration} months"
        };
    }

    public static TblPurchaseInvoice ToEntity(
        this DTOs.Features.PurchaseInvoice.PurchaseInvoiceRequestDTO purchaseInvoiceRequest,
        decimal totalAmount
    )
    {
        return new TblPurchaseInvoice
        {
            PurchaseInvoiceId = Ulid.NewUlid().ToString(),
            UserId = purchaseInvoiceRequest.UserId,
            InvoiceNo = Ulid.NewUlid().ToString(),
            CreatedDate = DateTime.UtcNow,
            TotalAmount = totalAmount,
            PaymentMethod = purchaseInvoiceRequest.PaymentMethod
        };
    }
}
