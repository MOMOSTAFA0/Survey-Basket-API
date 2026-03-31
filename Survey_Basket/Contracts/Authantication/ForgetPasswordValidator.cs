namespace Survey_Basket.Contracts.Authantication
{
	public class ForgetPasswordValidator : AbstractValidator<ForgetPasswordRequest>
	{
		public ForgetPasswordValidator()
		{
			RuleFor(F => F.Email)
				.NotEmpty()
				.EmailAddress();
		}
	}
}
