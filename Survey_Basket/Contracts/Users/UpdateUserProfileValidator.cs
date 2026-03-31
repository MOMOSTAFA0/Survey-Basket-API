namespace Survey_Basket.Contracts.Users
{
	public class UpdateUserProfileValidator : AbstractValidator<UpdateUserProfileRequest>
	{
		public UpdateUserProfileValidator()
		{
			RuleFor(U => U.FirstName)
				  .NotEmpty()
				  .Length(3, 100);

			RuleFor(U => U.LastName)
				.NotEmpty()
				.Length(3, 100);


		}
	}
}
