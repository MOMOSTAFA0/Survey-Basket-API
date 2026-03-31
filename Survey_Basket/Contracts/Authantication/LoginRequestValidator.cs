namespace Survey_Basket.Contracts.Authantication
{
	public class LoginRequestValidator : AbstractValidator<LoginRequist>
	{
		public LoginRequestValidator()
		{
			RuleFor(LR => LR.Email)
				  .NotEmpty()
				  .EmailAddress();

			RuleFor(LR => LR.Password)
				  .NotEmpty();

		}
	}
}
