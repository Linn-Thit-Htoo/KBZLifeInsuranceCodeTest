namespace KBZLifeInsuranceCodeTest.DTOs.Features.Account
{
    public class AccountDTO
    {
        public string UserId { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string UserRole { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
