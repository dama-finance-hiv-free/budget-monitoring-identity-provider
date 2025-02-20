using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace IdentityProvider.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private const string ApiKey = "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";

        public async Task<Response> SendEmailAsync(UserEmailOptions userEmailOptions)
        {
            var client = new SendGridClient(ApiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress("support@damafin.net", "Dama Support Team"),
                Subject = userEmailOptions.Subject,
                HtmlContent = userEmailOptions.Body
            };

            msg.AddTo(new EmailAddress(userEmailOptions.ToEmail, userEmailOptions.ToName));
            return await client.SendEmailAsync(msg);
        }
    }

    public interface IEmailSenderService
    {
        Task<Response> SendEmailAsync(UserEmailOptions userEmailOptions);
    }

    public class UserEmailOptions
    {
        public List<string> ToEmails { get; set; }
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
