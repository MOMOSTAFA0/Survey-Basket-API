namespace Survey_Basket.Contracts.Users
{
	public class AddUserPasswordValidator : AbstractValidator<AddUserPasswordRequist>
	{
		public AddUserPasswordValidator()
		{

			RuleFor(U => U.Password)
				.NotEmpty()
				.Matches(RegixPatterns.Password)
				.WithMessage("Password should be at least 8 digits and should contain lowercase,NonAlphanumeric,Uppercase,Length and UniqueChars");

			RuleFor(U => U.Code)
				.NotEmpty();
		}
	}
}
