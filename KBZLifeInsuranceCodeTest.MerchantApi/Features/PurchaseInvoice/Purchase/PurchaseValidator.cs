using FluentValidation;
using KBZLifeInsuranceCodeTest.DTOs.Features.PurchaseInvoice;

namespace KBZLifeInsuranceCodeTest.MerchantApi.Features.PurchaseInvoice.Purchase
{
    public class PurchaseValidator : AbstractValidator<PurchaseInvoiceRequestDTO>
    {
        public PurchaseValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID cannot be empty.")
                .NotNull().WithMessage("User ID cannot be null.");

            RuleFor(x => x.TotalAmount).NotEmpty().WithMessage("Total Amount cannot be empty.")
                .NotNull().WithMessage("Total Amount cannot be null.")
                .GreaterThan(0).WithMessage("Total Amount is invalid.");

            RuleFor(x => x.PaymentMethod).NotEmpty().WithMessage("Payment Method cannot be empty.")
                .NotNull().WithMessage("Payment Method cannot be null.");
        }
    }
}
