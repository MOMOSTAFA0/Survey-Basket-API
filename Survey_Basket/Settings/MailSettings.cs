using System.ComponentModel.DataAnnotations;

namespace Survey_Basket.Settings
{
	public class MailSettings
	{
		[EmailAddress, Required]
		public string Mail { get; set; }
		[Required]
		public string DisplayName { get; set; }
		[Required]
		public string Host { get; set; }
		[Required]
		public string Password { get; set; }
		[Range(100, 999)]
		public int Port { get; set; }
	}
}
