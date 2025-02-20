using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityProvider.Protectimus.Enums;
using Newtonsoft.Json;

namespace IdentityProvider.Protectimus
{
    public class ResourceServiceClient : AbstractServiceClient
    {
        public ResourceServiceClient(string apiUrl, string username, string apiKey, ResponseFormat responseFormat, string version) : base(apiUrl, username, apiKey, responseFormat, version)
        {
        }

        public virtual async Task<int> AddResourceTwo(string resourceName, string failedAttemptsBeforeLock)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("resourceName", resourceName),
                new KeyValuePair<string, string>("failedAttemptsBeforeLock", failedAttemptsBeforeLock)
            });

            var jsonResponse =
                JsonConvert.DeserializeObject<dynamic>(await PostProtectimusClient("resources", formContent));

            var status = (string)jsonResponse["responseHolder"]["status"];

            if (status != "OK") return 0;
            {
                var res = jsonResponse["responseHolder"]["response"]["id"];
                return res;
            }
        }

        protected override string ServiceName => "resource-service";
    }
}
