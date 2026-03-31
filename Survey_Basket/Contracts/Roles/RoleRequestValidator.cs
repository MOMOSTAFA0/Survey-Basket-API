namespace Survey_Basket.Contracts.Roles
{
	public class RoleRequestValidator : AbstractValidator<RoleRequest>
	{
		public RoleRequestValidator()
		{
			RuleFor(R => R.RoleName)
				.NotEmpty()
				.Length(3, 256);

			RuleFor(R => R.Permissions)
				.NotEmpty()
				.NotNull()
				.Must(P => P.Distinct()
				.Count() == P.Count())
				.WithMessage("you can't add Duplicated Permissions")
				.When(x => x.Permissions != null);

		}
	}
}
