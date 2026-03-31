using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Survey_Basket.Health
{
	public class MailProviderHealthChecks(IOptions<MailSettings> Mailsettings) : IHealthCheck
	{
		private readonly MailSettings _mailsettings = Mailsettings.Value;

		public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
		{
			try
			{
				using var smtp = new SmtpClient();
				await smtp.ConnectAsync(_mailsettings.Host, _mailsettings.Port, SecureSocketOptions.StartTls);
				await smtp.AuthenticateAsync(_mailsettings.Mail, _mailsettings.Password);
				await smtp.DisconnectAsync(true);
				return await Task.FromResult(HealthCheckResult.Healthy("Mail server Reacheable"));
			}
			catch
			{
				return await Task.FromResult(HealthCheckResult.Unhealthy("Mail server is not Reacheable"));
			}
		}
	}
}
