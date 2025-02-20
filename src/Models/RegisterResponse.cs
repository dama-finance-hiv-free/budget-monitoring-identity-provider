using IdentityProvider.Data.Entity;

namespace IdentityProvider.Models;

public class RegisterResponse : ApiResponse
{
    public object Data { get; set; }
}