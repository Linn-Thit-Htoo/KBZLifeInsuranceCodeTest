using FluentValidation;
using KBZLifeInsuranceCodeTest.DTOs.Features.Account;

namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.Login
{
    public class LoginValidator : AbstractValidator<LoginRequestDTO>
    {
        public LoginValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number cannot be empty.")
                .NotNull().WithMessage("Phone Number cannot be null.")
                .LessThan("11").WithMessage("Phone Number cannot be less than 11 numbers.")
                .GreaterThan("11").WithMessage("Phone Number cannot be greater than 11 numbers");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty.")
                .NotNull().WithMessage("Password cannot be null.");
        }
    }
}
