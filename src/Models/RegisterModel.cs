using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Models
{
    public class RegisterModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        public string Locale { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string Organization { get; set; }
        public string Role { get; set; }
    }

}
