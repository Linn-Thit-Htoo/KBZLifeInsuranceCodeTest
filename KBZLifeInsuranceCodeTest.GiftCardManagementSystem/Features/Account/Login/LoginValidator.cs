namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Features.Account.Login
{
    public class LoginValidator : AbstractValidator<LoginRequestDTO>
    {
        public LoginValidator()
        {

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone Number cannot be empty.")
                .NotNull().WithMessage("Phone Number cannot be null.")
                .Length(11).WithMessage("Phone Number is invalid.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty.")
                .NotNull().WithMessage("Password cannot be null.");
        }
    }
}
