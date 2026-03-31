namespace Survey_Basket.Contracts.Authantication
{
	public record RegistrationRequest(

		string Email,
		string Password,
		string FirstName,
		string LastName
	);
}
