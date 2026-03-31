namespace Survey_Basket.Contracts.Authantication
{
	public class RegistrationValidator : AbstractValidator<RegistrationRequest>
	{
		public RegistrationValidator()
		{
			RuleFor(R => R.Email)
				  .NotEmpty()
				  .EmailAddress();

			RuleFor(R => R.Password)
				  .NotEmpty()
				  .Matches(RegixPatterns.Password)
				  .WithMessage("Password should be at least 8 digits and should contain lowercase,NonAlphanumeric,Uppercase,Length and UniqueChars");

			RuleFor(R => R.FirstName)
				.NotEmpty()
				.Length(3, 100);

			RuleFor(R => R.Password)
				  .NotEmpty()
				  .Length(3, 100);
		}
	}
}
