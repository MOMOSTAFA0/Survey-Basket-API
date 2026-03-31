namespace Survey_Basket.Contracts.Users
{
	public class CreateUserValidator : AbstractValidator<CreateUserRequest>
	{
		public CreateUserValidator()
		{
			RuleFor(U => U.FirstName)
				.NotEmpty()
				.Length(3, 100);

			RuleFor(U => U.LastName)
				.NotEmpty()
				.Length(3, 100);

			RuleFor(U => U.Email)
				.NotEmpty()
				.EmailAddress();

			RuleFor(U => U.Roles)
				.NotEmpty()
				.Must(U => U.Distinct().Count() == U.Count())
				.WithMessage("you can't add Duplicated Permissions")
				.NotNull();
		}
	}
}
