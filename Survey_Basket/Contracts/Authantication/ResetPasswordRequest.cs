namespace Survey_Basket.Contracts.Authantication
{
	public record ResetPassword_Request(
		string Email,
		string Code,
		string NewPassword
	);
}
