namespace Survey_Basket.Contracts.Authantication
{
	public class ResendConfirmationEmailValidator : AbstractValidator<ResentConfirmationEmailRequest>
	{
		public ResendConfirmationEmailValidator()
		{
			RuleFor(LR => LR.Email)
				  .NotEmpty()
				  .EmailAddress();
		}
	}
}
