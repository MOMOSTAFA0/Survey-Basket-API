namespace Survey_Basket.Contracts.Users
{
	public record UserResponse(

		string id,
		string FirstName,
		string LastName,
		string Email,
		bool IsDesabled,
		IEnumerable<string> Roles
	);
}
