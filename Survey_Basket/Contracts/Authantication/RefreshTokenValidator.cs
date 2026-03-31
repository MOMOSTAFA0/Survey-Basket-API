namespace Survey_Basket.Contracts.Authantication
{
	public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
	{
		public RefreshTokenValidator()
		{
			RuleFor(x => x.RefreshToken).NotEmpty();
			RuleFor(x => x.Token).NotEmpty();
		}
	}
}
