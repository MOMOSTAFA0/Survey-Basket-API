using System.ComponentModel.DataAnnotations;

namespace Survey_Basket.Authantication
{
	public class JWTOptions
	{
		public static string JWTSection = "JWT";
		[Required]
		public string Key { get; init; } = string.Empty;
		[Required]
		public string Issuer { get; init; } = string.Empty;
		[Required]
		public string Audience { get; init; } = string.Empty;
		[Required]
		[Range(1, int.MaxValue)]
		public int DurationInMinutes { get; init; }
	}
}
