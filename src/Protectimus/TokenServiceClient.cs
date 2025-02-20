using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityProvider.Protectimus.Enums;
using Newtonsoft.Json;

namespace IdentityProvider.Protectimus
{
    public class TokenServiceClient : AbstractServiceClient
    {
        public TokenServiceClient(string apiUrl, string username, string apiKey, ResponseFormat responseFormat,
            string version) : base(apiUrl, username, apiKey, responseFormat, version)
        {
        }

        public virtual async Task<int> AddHardwareToken(string userId, string userLogin,
            string type, string serialNumber, string name, string secret,
            string otp, string isExistedToken, string pin, string pinOtpFormat)
        {
            var formContent = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    {
                        "userId", userId
                    },
                    {
                        "userLogin", userLogin
                    },
                    {
                        "type", type
                    },
                    {
                        "serialNumber", serialNumber
                    },
                    {
                        "name", name
                    },
                    {
                        "secret", secret
                    },
                    {
                        "otp", otp
                    },
                    {
                        "isExistedToken", isExistedToken
                    },
                    {
                        "pinOtpFormat", pinOtpFormat
                    }
                });

            var jsonResponse =
                JsonConvert.DeserializeObject<dynamic>(await PostProtectimusClient("tokens/hardware",
                    formContent));

            var status = (string)jsonResponse["responseHolder"]["status"];

            if (status != "OK") return 0;
            var res = jsonResponse["responseHolder"]["response"]["id"];
            return res;
        }

        public virtual async Task<int> AddUnifyToken(string userId, string userLogin,
            string unifyTokenType, string unifyKeyAlgo, string unifyKeyFormat,
            string serialNumber, string tokenName,
            string secret, string otp, string otpLength,
            string pin, string pinOtpFormat,
            string counter, string challenge)
        {
            var formContent = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    {
                        "unifyTokenType", unifyTokenType
                    },
                    {
                        "unifyKeyAlgo", unifyKeyAlgo
                    },
                    {
                        "unifyKeyFormat", unifyKeyFormat
                    },
                    {
                        "serialNumber", serialNumber
                    },
                    {
                        "tokenName", tokenName
                    },
                    {
                        "secret", secret
                    },
                    {
                        "otp", otp
                    },
                    {
                        "otpLength", otpLength
                    },
                    {
                        "pin", pin
                    },
                    {
                        "pinOtpFormat", pinOtpFormat
                    },
                    {
                        "counter", counter
                    },
                    {
                        "challenge", challenge
                    }
                });

            var jsonResponse =
                JsonConvert.DeserializeObject <dynamic>(await PostProtectimusClient("tokens/unify",
                    formContent));



            var status = (string)jsonResponse["responseHolder"]["status"];

            if (status != "OK") return 0;
            var res = jsonResponse["responseHolder"]["response"]["id"];
            return res;
        }

        public virtual string GoogleAuthenticatorSecretKey
        {
            get
            {
                var jsonResponse =
                    JsonConvert.DeserializeObject<dynamic>(
                        GetProtectimusClient("secret-key/google-authenticator").Result);

                var status = (string)jsonResponse["responseHolder"]["status"];

                if (status != "OK") return string.Empty;
                var result = jsonResponse["responseHolder"]["response"]["key"];
                return result;
            }
        }

        public virtual async Task<int> TokensQuantity()
        {
            var jsonResponse =
                JsonConvert.DeserializeObject<dynamic>(await
                    GetProtectimusClient("tokens/quantity"));

            var status = (string)jsonResponse["responseHolder"]["status"];

            if (status != "OK") return -1;
            var result = jsonResponse["responseHolder"]["response"]["quantity"];
            return result;
        }

        public virtual string SecretKey
        {
            get
            {
                var jsonResponse =
                    JsonConvert.DeserializeObject <dynamic>(
                        GetProtectimusClient("secret-key").Result);

                var status = (string)jsonResponse["responseHolder"]["status"];

                if (status != "OK") return string.Empty;
                var result = jsonResponse["responseHolder"]["response"]["quantity"];
                return result;
            }
        }

        public virtual string ProtectimusSmartSecretKey
        {
            get
            {
                var jsonResponse =
                    JsonConvert.DeserializeObject <dynamic>(
                        GetProtectimusClient("secret-key/protectimus-smart").Result);

                var status = (string)jsonResponse["responseHolder"]["status"];

                if (status != "OK") return string.Empty;
                var result = jsonResponse["responseHolder"]["response"]["key"];
                return result;
            }
        }

        protected override string ServiceName => "token-service";
    }
}
