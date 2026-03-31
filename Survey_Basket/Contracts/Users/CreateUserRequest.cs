namespace Survey_Basket.Contracts.Users
{
	public record CreateUserRequest(
		string FirstName,
		string LastName,
		string Email,
		IEnumerable<string> Roles
	);
}
