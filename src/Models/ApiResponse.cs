using System.Collections.Generic;

namespace IdentityProvider.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            Success = true;
            ValidationErrors = new List<string>();
            Message = string.Empty;
        }

        public ApiResponse(string message = null)
        {
            Success = true;
            Message = message;
        }

        public ApiResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }
    }
}
