﻿namespace KBZLifeInsuranceCodeTest.DTOs.Features.GiftCard;

public class GiftCardDTO
{
    public string GiftCardId { get; set; } = null!;

    public string GiftCardNo { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? ExpiryDate { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public byte[] Qrcode { get; set; } = null!;

    public string Duration { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public string? PhoneNumber { get; set; }

    public int? CashbackPercentage { get; set; }
}
