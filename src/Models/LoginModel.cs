using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class LoginResponse : ApiResponse
    {
        public UserVm CurrentUser { get; set; }
    }
}