namespace Survey_Basket.Contracts.Authantication
{
	public class ResetPasswordValidator : AbstractValidator<ResetPassword_Request>
	{
		public ResetPasswordValidator()
		{
			RuleFor(R => R.Email)
				.NotEmpty()
				.EmailAddress();

			RuleFor(R => R.NewPassword)
				.NotEmpty()
				.Matches(RegixPatterns.Password)
				.WithMessage("Password Should be at least 8 digits and should contain lowercase,NonAlphnumaric and Upcrease characters");

			RuleFor(R => R.Code)
				.NotEmpty();

		}
	}
}
