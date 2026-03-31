namespace Survey_Basket.Helpers
{
	public static class EmailProvider
	{

		public static async Task<string> GenerateEmailVervicationCode(ApplicationUser user, UserManager<ApplicationUser> usermanger)
		{
			var code = await usermanger!.GenerateEmailConfirmationTokenAsync(user!);
			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
			return code;
		}

		public static async Task SendEmailAsync(
			ApplicationUser user,
			UserManager<ApplicationUser> userManager,
			IEmailSender emailSender,
			string TemplateName,
			StringValues? Origin, string Describtion,
			string Code,
			string Subject)
		{
			var EmailBody = EmailBodyBuilder.GenerateEmailBody(TemplateName,
			new Dictionary<string, string>
			{
				{ "{{name}}",user.FirstName},
				{ "{{action_url}}",$"{Origin}/{Describtion}?userId={user.Id}&code={Code}"}
			});
			BackgroundJob.Enqueue(() => emailSender.SendEmailAsync(user.Email!, Subject, EmailBody));
			await Task.CompletedTask;
		}
	}
}
