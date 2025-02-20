using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IdentityProvider.Protectimus
{
    public abstract class ClientServiceBase
    {
        private readonly string _apiLogin = string.Empty;
        private readonly string _apiKey = string.Empty;
        private readonly string _apiUrl = string.Empty;
        private readonly string _resourceId = string.Empty;

        protected ClientServiceBase(string apiLogin, string apiKey, string apiUrl, string resourceId)
        {
            _apiLogin = apiLogin;
            _apiKey = apiKey;
            _apiUrl = apiUrl;
            _resourceId = resourceId;
        }

        protected ClientServiceBase()
        {

        }

        internal async Task<string> GetProtectimusClient(string method, FormUrlEncodedContent post)
        {
            ServicePointManager.ServerCertificateValidationCallback += (_, _, _, _) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls12;

            //| SecurityProtocolType.Ssl3; removed this for compatibility reasons. 

            const string additionalApiUrl = "/api/v1/auth-service/";
            const string returnType = "json";
            var url = $"{_apiUrl}{additionalApiUrl}{method}.{returnType}";
            var byteArray = Encoding.ASCII.GetBytes($"{_apiLogin}:{GetHashApiKey()}");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var response = await client.PostAsync(url, post);
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        internal string GetHashApiKey()
        {
            var dateTimeNow = DateTime.UtcNow;
            var date = dateTimeNow.ToString("yyyyMMdd");
            var time = dateTimeNow.ToString("HH");
            var key = $"{_apiKey}:{date}:{time}";

            return GetStringSha256Hash(key);
        }

        internal static string GetStringSha256Hash(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            using var sha = SHA256.Create();
            var textData = Encoding.UTF8.GetBytes(text);
            var hash = sha.ComputeHash(textData);
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }

        public async Task<AuthResult> AuthUserToken(string username, string otp)
        {
            var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"resourceId", _resourceId}, {"userLogin", username}, {"otp", otp}
            });
            var client = await GetProtectimusClient("authenticate/user-token", formContent);
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(client);
            var status = (string) jsonResponse["responseHolder"]["status"];
            if (status == "OK")
            {
                var res = (bool) jsonResponse["responseHolder"]["response"]["result"];
                Console.WriteLine(@"Protectimus AuthUserToken for UPN: " + username);
                return new AuthResult {Result = res};
            }

            var error = (string) jsonResponse["responseHolder"]["error"]["message"];
            return new AuthResult {Result = false, Message = error};
        }
    }

    public class AuthResult
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
