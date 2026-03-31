namespace Survey_Basket.Contracts.Users
{
	public class ChagePasswordValidator : AbstractValidator<ChangePasswordRequest>
	{
		public ChagePasswordValidator()
		{
			RuleFor(C => C.CurrentPassword)
				  .NotEmpty();

			RuleFor(C => C.NewPassword)
				.NotEmpty()
				.Matches(RegixPatterns.Password)
				.WithMessage("Password Should be at least 8 digits and should contain lowercase,NonAlphnumaric and Upcrease characters")
				.NotEqual(n => n.CurrentPassword)
				.WithMessage("new Password cannot be the same as current password");
		}
	}
}
