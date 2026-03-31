namespace Survey_Basket.Contracts.Users
{
	public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequestRequest>
	{
		public UpdateUserRequestValidator()
		{
			RuleFor(U => U.FirstName)
				  .NotEmpty()
				  .Length(3, 100);

			RuleFor(U => U.Lastname)
				.NotEmpty()
				.Length(3, 100);

			RuleFor(U => U.Email)
				.NotEmpty()
				.EmailAddress();

			RuleFor(U => U.UserRoles)
				.NotEmpty()
				.Must(U => U.Distinct().Count() == U.Count()).When(u => u != null)
				.WithMessage("You Can't add dublicate role for the same user");

		}
	}
}
