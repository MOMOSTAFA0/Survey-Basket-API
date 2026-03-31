using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Survey_Basket.Services
{
	public class EmailService(IOptions<MailSettings> Mailsettings) : IEmailSender
	{
		private readonly MailSettings _mailsettings = Mailsettings.Value;

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var message = new MimeMessage
			{
				Sender = MailboxAddress.Parse(_mailsettings.Mail),
				Subject = subject
			};
			message.To.Add(MailboxAddress.Parse(email));

			var builder = new BodyBuilder
			{
				HtmlBody = htmlMessage
			};
			message.Body = builder.ToMessageBody();

			using var smtp = new SmtpClient();
			smtp.Connect(_mailsettings.Host, _mailsettings.Port, SecureSocketOptions.StartTls);
			smtp.Authenticate(_mailsettings.Mail, _mailsettings.Password);
			await smtp.SendAsync(message);
			smtp.Disconnect(true);
		}
	}
}
