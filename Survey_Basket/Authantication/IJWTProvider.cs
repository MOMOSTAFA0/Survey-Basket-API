namespace Survey_Basket.Authantication
{
	public interface IJWTProvider
	{
		(string token, int expiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions);
		string? ValidateToken(string token);
	}
}
