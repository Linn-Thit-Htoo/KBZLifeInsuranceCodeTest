using FluentValidation;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.CreateAccount
{
    public class CreateAccountValidator : AbstractValidator<AccountRequestDTO>
    {
        public CreateAccountValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User Name cannot be empty.")
                .NotNull().WithMessage("User Name cannot be null.");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number cannot be empty.")
                .NotNull().WithMessage("Phone Number cannot be null.")
                .Length(11).WithMessage("Phone Number is invalid.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty.")
                .NotNull().WithMessage("Password cannot be null.");

            RuleFor(x => x.UserRole).NotEmpty().WithMessage("User Role cannot be empty.")
                .NotNull().WithMessage("User Role cannot be null.");
        }
    }
}
