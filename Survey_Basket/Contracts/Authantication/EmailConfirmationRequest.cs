namespace Survey_Basket.Contracts.Authantication
{
	public record EmailConfirmationRequest(

		string UserID,
		string Code
	);

}
