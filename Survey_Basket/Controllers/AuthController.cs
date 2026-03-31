using Microsoft.Extensions.Options;

namespace Survey_Basket.Controllers
{
	[Route("[controller]")]
	[ApiController]
	[EnableRateLimiting(RateLimiterPolicies.IPLimit)]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly JWTOptions _jWTOptions;
		private readonly ILogger<AuthController> _logger;

		public AuthController(IAuthService authService, IOptions<JWTOptions> JWTOptions, ILogger<AuthController> logger)
		{
			_authService = authService;
			_jWTOptions = JWTOptions.Value;
			_logger = logger;
		}
		[HttpPost("Token")]

		public async Task<IActionResult> LoginAsync([FromBody] LoginRequist loginRequest, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Logging within email:{email} and Logginw within password:{password}", loginRequest.Email, loginRequest.Password);

			var authresult = await _authService.GetTokenAsync(loginRequest.Email, loginRequest.Password, cancellationToken);
			return authresult.IsSuccess
				? Ok(authresult.Value)
				: authresult.ToProblem();
		}
		[HttpPost("RefreshToken")]
		public async Task<IActionResult> RefreshAsync([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
		{
			var authResult = await _authService.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
			return authResult.IsSuccess
				? Ok(authResult.Value)
				: authResult.ToProblem();
		}
		[HttpPost("revoke-refresh-token")]
		public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
		{
			var IsRevoked = await _authService.RevokeRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
			return IsRevoked.IsSuccess
				? Ok()
				: IsRevoked.ToProblem();
		}
		[HttpPost("Register")]
		[DisableRateLimiting]
		public async Task<IActionResult> Register([FromBody] RegistrationRequest request, CancellationToken cancellationToken)
		{
			var IsRegistered = await _authService.RegisterAsync(request, cancellationToken);
			return IsRegistered.IsSuccess
				? Ok()
				: IsRegistered.ToProblem();
		}
		[HttpPost("confirm-email")]
		public async Task<IActionResult> ConfirmEmail([FromBody] EmailConfirmationRequest request, CancellationToken cancellationToken)
		{
			var IsRegistered = await _authService.ConfirmEmailAsync(request, cancellationToken);
			return IsRegistered.IsSuccess
				? Ok()
				: IsRegistered.ToProblem();
		}
		[HttpPost("resend-confirmation-email")]
		public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResentConfirmationEmailRequest request, CancellationToken cancellationToken)
		{
			var IsRegistered = await _authService.ResendConfirmationEmailAsync(request, cancellationToken);
			return IsRegistered.IsSuccess
				? Ok()
				: IsRegistered.ToProblem();
		}

		[HttpPost("forget-password")]
		public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordRequest request, CancellationToken cancellationToken)
		{
			var result = await _authService.SendResetPasswordCodeAsync(request, cancellationToken);
			return result.IsSuccess
				? Ok()
				: result.ToProblem();
		}

		[HttpPost("reset-password")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPassword_Request request, CancellationToken cancellationToken)
		{
			var result = await _authService.ResetPasswordAsync(request, cancellationToken);
			return result.IsSuccess
				? Ok()
				: result.ToProblem();
		}

	}
}