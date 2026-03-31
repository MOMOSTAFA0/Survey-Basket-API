namespace Survey_Basket.Controllers
{
	[Route("ME")]
	[ApiController]
	[Authorize]
	public class AccountController(IUserService userService) : ControllerBase
	{
		private readonly IUserService _userService = userService;

		[HttpGet("")]
		public async Task<IActionResult> GetProfileINFO(CancellationToken cancellationToken)
		{
			var user = await _userService.GetUserDataAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!, cancellationToken);
			return Ok(user);
		}
		[HttpPut("EDIT")]
		public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserProfileRequest request, CancellationToken cancellationToken)
		{
			await _userService.UpdateUserProfileAsync(request, User.FindFirstValue(ClaimTypes.NameIdentifier)!, cancellationToken);
			return NoContent();
		}
		[HttpPut("change-current-email")]
		public async Task<IActionResult> UpdateUserEmail(UpdatEmailRequest request, CancellationToken cancellationToken)
		{
			var updated = await _userService.UpdateUserEmailAsync(request, User.FindFirstValue(ClaimTypes.NameIdentifier)!, cancellationToken);
			return updated.IsSuccess ? NoContent() : updated.ToProblem();

		}
		[HttpPut("change-password-request")]
		public async Task<IActionResult> ChageUserPassword(ChangePasswordRequest request, CancellationToken cancellationToken)
		{
			var updated = await _userService.ChangePasswordAsync(User.FindFirstValue(ClaimTypes.NameIdentifier)!, request, cancellationToken);

			return updated.IsSuccess ? NoContent() : updated.ToProblem();

		}

	}
}
