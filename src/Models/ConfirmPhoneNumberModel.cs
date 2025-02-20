using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Models
{
    public class ConfirmPhoneNumberModel
    {
        [Required]
        public string Token { get; set; }
    }
}