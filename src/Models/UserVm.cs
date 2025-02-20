namespace IdentityProvider.Models
{
    public class UserVm
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Locale { get; set; }
    }

    public class UserResponse : ApiResponse
    {
        public UserVm[] Users { get; set; }
    }
}