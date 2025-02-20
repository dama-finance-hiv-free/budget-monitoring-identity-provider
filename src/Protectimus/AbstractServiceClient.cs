using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IdentityProvider.Protectimus.Enums;

namespace IdentityProvider.Protectimus
{
    public abstract class AbstractServiceClient
    {
        protected readonly string ApiKey = string.Empty;
        protected readonly string ApiUrl = string.Empty;
        protected readonly string UserName = string.Empty;
        protected readonly ResponseFormat ResponseFormat = ResponseFormat.Json;
        protected readonly string ApiVersion = string.Empty;

        protected AbstractServiceClient(string apiUrl, string userName, string apiKey, ResponseFormat responseFormat,
            string apiVersion)
        {
            UserName = userName;
            ApiKey = apiKey;
            ApiUrl = apiUrl;
            ApiVersion = apiVersion;
            ResponseFormat = responseFormat;
        }

        protected AbstractServiceClient()
        {

        }

        protected async Task<string> PostProtectimusClient(string method, FormUrlEncodedContent post)
        {
            var client = GetClient();
            var url = GetUrl(method);
            var response = await client.PostAsync(url, post);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        internal string GetUrl(string method)
        {
            var additionalApiUrl = $"/api/{ApiVersion}/{ServiceName}/";
            var returnType = ResponseFormat;
            var url = $"{ApiUrl}{additionalApiUrl}{method}{returnType.Extension}";
            return url;
        }

        protected async Task<string> GetProtectimusClient(string method)
        {
            var client = GetClient();
            var url = GetUrl(method);
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        protected HttpClient GetClient()
        {
            ServicePointManager.ServerCertificateValidationCallback += (_, _, _, _) => true;

            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 |
            //                                       SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            var byteArray = Encoding.ASCII.GetBytes($"{UserName}:{GetHashApiKey()}");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            return client;
        }

        protected string GetHashApiKey()
        {
            var dateTimeNow = DateTime.UtcNow;
            var date = dateTimeNow.ToString("yyyyMMdd");
            var time = dateTimeNow.ToString("HH");
            var key = $"{ApiKey}:{date}:{time}";
            return GetStringSha256Hash(key);
        }

        protected static string GetStringSha256Hash(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            using var sha = System.Security.Cryptography.SHA256.Create();
            var textData = Encoding.UTF8.GetBytes(text);
            var hash = sha.ComputeHash(textData);
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }

        protected abstract string ServiceName { get; }
    }
}
