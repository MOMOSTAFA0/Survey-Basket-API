namespace Survey_Basket.Services
{
	public interface IAuthService
	{
		Task<Result<AuthResponse>> GetTokenAsync(string Email, string Password, CancellationToken cancellationToken);
		Task<Result<AuthResponse>> GetRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellationToken);
		Task<Result> RevokeRefreshTokenAsync(string token, string RefreshToken, CancellationToken cancellationToken);
		Task<Result> RegisterAsync(RegistrationRequest request, CancellationToken cancellationToken);
		Task<Result> ConfirmEmailAsync(EmailConfirmationRequest request, CancellationToken cancellationToken);
		Task<Result> ResendConfirmationEmailAsync(ResentConfirmationEmailRequest request, CancellationToken cancellationToken);
		Task<Result> SendResetPasswordCodeAsync(ForgetPasswordRequest request, CancellationToken cancellationToken);
		Task<Result> ResetPasswordAsync(ResetPassword_Request request, CancellationToken cancellationToken);

	}
}
