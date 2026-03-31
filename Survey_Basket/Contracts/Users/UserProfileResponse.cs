namespace Survey_Basket.Contracts.Users
{
	public record UserProfileResponse(
		string UserName,
		string Email,
		string FirstName,
		string LastName
		);
}
