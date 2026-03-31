namespace Survey_Basket.Contracts.Authantication
{
	public record RefreshTokenRequest(
	  string Token,
	  string RefreshToken
	);
}
