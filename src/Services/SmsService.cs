using System;
using System.IO;
using System.Threading.Tasks;
using IdentityProvider.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace IdentityProvider.Services
{
    public class SmsService : ISmsService
    {
        private readonly TextMessageSettings _messageSettings;
        public SmsService()
        {
            var messageSettings = new TextMessageSettings();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            configuration.Bind("TextMessageCredentials", messageSettings);
            _messageSettings = messageSettings;
        }

        public async Task<IRestResponse> SendSmsAsync(UserSmsOptions userSmsOptions)
        {

            var client = new RestClient
            {
                BaseUrl = new Uri(_messageSettings.MessageApi),
            };

            var request = new RestRequest
            {
                Resource = "sendsms?version=2&phone={AccountSid}&password={AuthToken}&from={SenderId}&to={destination}&text={body}&id=1"
            };
            request.AddParameter("AccountSid", $"{_messageSettings.AccountSid}", ParameterType.UrlSegment);
            request.AddParameter("AuthToken", $"{_messageSettings.AuthToken}", ParameterType.UrlSegment);
            request.AddParameter("SenderId", $"{_messageSettings.SenderId}", ParameterType.UrlSegment);

            request.AddParameter("destination", $"+237{userSmsOptions.Destination}", ParameterType.UrlSegment);
            request.AddParameter("body", userSmsOptions.Body, ParameterType.UrlSegment);

            request.Method = Method.GET;

            return await client.ExecuteAsync(request);
        }

    }

    public interface ISmsService
    {
        Task<IRestResponse> SendSmsAsync(UserSmsOptions userSmsOptions);
    }

    public class UserSmsOptions
    {
        public string Destination { get; set; }
        public string Body { get; set; }
    }
}
