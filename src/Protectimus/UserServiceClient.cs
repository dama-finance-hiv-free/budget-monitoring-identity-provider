using System.Collections.Generic;
using System.Net.Http;
using IdentityProvider.Protectimus.Enums;
using Newtonsoft.Json;

namespace IdentityProvider.Protectimus
{
    public class UserServiceClient : AbstractServiceClient
    {
        public UserServiceClient(string apiUrl, string username, string apiKey, ResponseFormat responseFormat, string version) : base(apiUrl, username, apiKey, responseFormat, version)
        {
        }

        public virtual int AddUser(string login, string email, string phoneNumber, string password,
            string firstName, string secondName, string apiSupport)
        {
            var formContent = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    {
                        "login", login
                    },
                    {
                        "email", email
                    },
                    {
                        "phoneNumber", phoneNumber
                    },
                    {
                        "password", password
                    },
                    {
                        "firstName", firstName
                    },
                    {
                        "secondName", secondName
                    },
                    {
                        "apiSupport", apiSupport
                    }
                });

            dynamic jsonResponse =
                JsonConvert.DeserializeObject<dynamic>(PostProtectimusClient("users",
                    formContent).Result);

            var status = (string)jsonResponse["responseHolder"]["status"];

            if (status != "OK") return 0;
            var res = jsonResponse["responseHolder"]["response"]["id"];
            return res;
        }



        protected override string ServiceName => "user-service";
    }
}
