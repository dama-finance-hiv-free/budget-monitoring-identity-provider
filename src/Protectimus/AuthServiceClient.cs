using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityProvider.Protectimus.Enums;
using Newtonsoft.Json;

namespace IdentityProvider.Protectimus
{
    public class AuthServiceClient : AbstractServiceClient
    {
        public AuthServiceClient(string apiUrl, string username, string apiKey, ResponseFormat responseFormat,
            string version) : base(apiUrl, username, apiKey, responseFormat, version)
        {
        }

        public virtual async Task<string> GetBalance()
        {
            var balance = await GetProtectimusClient("balance");
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(balance);

            var status = (string)jsonResponse["responseHolder"]["status"];

            if (status != "OK") return string.Empty;
            var result = jsonResponse["responseHolder"]["response"]["balance"];

            return result;
        }

        public virtual async Task<string> Prepare(string resourceId, string resourceName,
            string tokenId, string userId, string userLogin)
        {
            var formContent = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    {
                        "resourceId", resourceId
                    },
                    {
                        "resourceName", resourceName
                    },
                    {
                        "tokenId", tokenId
                    },
                    {
                        "userId", userId
                    },
                    {
                        "userLogin", userLogin
                    }
                });

            var jsonResponse =
                JsonConvert.DeserializeObject<dynamic>(await PostProtectimusClient("prepare", formContent));

            var status = (string)jsonResponse["responseHolder"]["status"];

            if (status != "OK") return string.Empty;
            var res = jsonResponse["responseHolder"]["response"]["challenge"];
            return res;
        }

        public async Task<bool> AuthUserToken(string userLogin, string otp, string resourceId)
        {
            try
            {
                var formContent = new FormUrlEncodedContent(
                    new Dictionary<string, string>
                    {
                        {"resourceId", resourceId},
                        { "userLogin", userLogin},
                        { "otp", otp}
                    });
                var result = await PostProtectimusClient("authenticate/user-token", formContent);
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(result);
                var status = jsonResponse["responseHolder"]["status"];
                if (status != "OK") return false;
                var res = (bool)jsonResponse["responseHolder"]["response"]["result"];
                return res;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected override string ServiceName => "auth-service";
    }
}
