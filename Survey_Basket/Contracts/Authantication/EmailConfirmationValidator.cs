namespace Survey_Basket.Contracts.Authantication
{
	public class EmailConfirmationValidator : AbstractValidator<EmailConfirmationRequest>
	{
		public EmailConfirmationValidator()
		{
			RuleFor(E => E.UserID)
				.NotEmpty();

			RuleFor(E => E.Code)
				.NotEmpty();
		}
	}
}
