namespace Survey_Basket.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles = DefaultRole.Admin)]
	public class UsersController(IUserService userService) : ControllerBase
	{
		private readonly IUserService _userService = userService;

		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			return Ok(await _userService.GEtAllUsersAsync());
		}
		[HttpGet("{userId}")]
		public async Task<IActionResult> GetUserDetail([FromRoute] string userId)
		{
			var result = await _userService.GetUserDetailAsync(userId);
			return result.IsSuccess ? Ok(result) : result.ToProblem();
		}

		[HttpPost("")]
		public async Task<IActionResult> AddUserData([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
		{
			var result = await _userService.AddUserAsync(request, cancellationToken);
			return result.IsSuccess ? Ok() : result.ToProblem();
		}

		[HttpPut("")]
		public async Task<IActionResult> AddPassword([FromBody] AddUserPasswordRequist requist, CancellationToken cancellationToken)
		{
			var result = await _userService.AddPasswordToUser(requist, cancellationToken);
			return result.IsSuccess ? NoContent() : result.ToProblem();
		}
		[HttpPut("/{id}")]
		public async Task<IActionResult> UpdateUserDat([FromRoute] string id, [FromBody] UpdateUserRequestRequest requist, CancellationToken cancellationToken)
		{
			var result = await _userService.UpdateUserAsync(id, requist, cancellationToken);
			return result.IsSuccess ? NoContent() : result.ToProblem();

		}
		[HttpPut("toggle-status/{UserID}")]
		public async Task<IActionResult> ToggleStatus([FromRoute] string UserID, CancellationToken cancellationToken)
		{
			var Result = await _userService.ToggleStatusAsync(UserID, cancellationToken);
			return Result.IsSuccess ? NoContent() : Result.ToProblem();
		}
		[HttpPut("enable-lockout/{UserID}")]
		public async Task<IActionResult> UnlockUser([FromRoute] string UserID, CancellationToken cancellationToken)
		{
			var Result = await _userService.UnLockUserAsync(UserID, cancellationToken);
			return Result.IsSuccess ? NoContent() : Result.ToProblem();
		}

	}
}
