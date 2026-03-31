namespace Survey_Basket.Contracts.Users
{
	public class UpdatEmailValidator : AbstractValidator<UpdatEmailRequest>
	{
		public UpdatEmailValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress();
		}
	}
}
