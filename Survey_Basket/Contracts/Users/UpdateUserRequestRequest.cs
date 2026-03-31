namespace Survey_Basket.Contracts.Users
{
	public record UpdateUserRequestRequest(
		string FirstName,
		string Lastname,
		string Email,
		IEnumerable<string> UserRoles
	);
}
