namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.GiftCard.UpdateGiftCard;

public class UpdateGiftCardValidator : AbstractValidator<GiftCardRequestDTO>
{
    public UpdateGiftCardValidator()
    {
        RuleFor(x => x.CashbackPercentage)
            .GreaterThan(0)
            .WithMessage("Cashback cannot be empty.")
            .LessThan(100)
            .WithMessage("Cashback is invalid.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone Number cannot be empty.")
            .NotNull()
            .WithMessage("Phone Number cannot be null.")
            .Length(11)
            .WithMessage("Phone Number is invalid.");
    }
}
