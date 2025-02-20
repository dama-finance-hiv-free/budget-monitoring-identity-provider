using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Models;

public class TwoFactorModel
{
    [Required]
    public string Token { get; set; }

    public bool RememberLogin { get; set; }
    public string ReturnUrl { get; set; }
}

//public class ForgotPasswordModel
//{
//    [Required]
//    public string Email { get; set; }
//}