namespace IdentityProvider.Models
{
    public class IdentitySettings
    {
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequireNonAlphanumeric { get; set; }
        public bool PasswordRequireDigit { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool SignInRequireConfirmedEmail { get; set; }
        public bool SignInRequireConfirmedPhoneNumber { get; set; }
        public bool UserRequireUniqueEmail { get; set; }
    }
}