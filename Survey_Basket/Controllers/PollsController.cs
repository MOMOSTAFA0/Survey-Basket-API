using Survey_Basket.Contracts.Requests;

namespace Survey_Basket.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PollsController : ControllerBase
	{
		private readonly IPollService _pollService;

		public PollsController(IPollService pollService)
		{
			_pollService = pollService;
		}

		[HttpGet("GetAll")]
		[HasPermissionAttrebuite(Permissions.GetPolls)]
		public async Task<IActionResult> Getall(CancellationToken cancellationToken)
		{
			var polls = await _pollService.GetAllAsync(cancellationToken);
			return polls.IsSuccess
				? Ok(polls.Value)
				: polls.ToProblem();
		}
		[HttpGet("Current")]
		[Authorize(Roles = DefaultRole.Member)]
		[EnableRateLimiting(RateLimiterPolicies.UserLimit)]
		public async Task<IActionResult> GetCurrent(CancellationToken cancellationToken)
		{
			var polls = await _pollService.GetCurrentAsync(cancellationToken);
			return polls.IsSuccess
				? Ok(polls.Value)
				: polls.ToProblem();
		}
		[HttpGet("GetByID/{id}")]
		[HasPermissionAttrebuite(Permissions.GetPolls)]
		public async Task<IActionResult> GetByID([FromRoute] int id, CancellationToken cancellationToken)
		{
			var result = await _pollService.GetByIdAsync(id, cancellationToken);
			return result.IsSuccess
				? Ok(result.Value)
				: result.ToProblem();
		}

		[HttpPost("Create")]
		[HasPermissionAttrebuite(Permissions.AddPolls)]

		public async Task<IActionResult> CreatePoll([FromBody] PollRequest request, CancellationToken cancellationToken)
		{

			var poll = await _pollService.CreatPollAsync(request, cancellationToken);
			return poll.IsSuccess
				? CreatedAtAction(nameof(CreatePoll), new { ID = poll.Value.Id }, poll.Value)
				: poll.ToProblem();
		}

		[HttpPut("Update/{id}")]
		[HasPermissionAttrebuite(Permissions.UpdatePolls)]

		public async Task<IActionResult> UpdatePoll([FromRoute] int id, [FromBody] PollRequest request, CancellationToken cancellationToken)
		{
			var isUpdated = await _pollService.UpdatePollAsync(id, request, cancellationToken);

			return isUpdated.IsSuccess ?
				NoContent() :
				isUpdated.ToProblem();

		}

		[HttpDelete("Delete/{id}")]
		[HasPermissionAttrebuite(Permissions.DeletePolls)]

		public async Task<IActionResult> DeletePoll([FromRoute] int id, CancellationToken cancellationToken)
		{
			var isDeleted = await _pollService.DeletePollAsync(id, cancellationToken);
			return isDeleted.IsSuccess ? NoContent() : isDeleted.ToProblem();
		}

		[HttpPut("{id}/toggle-publish")]
		[HasPermissionAttrebuite(Permissions.UpdatePolls)]

		public async Task<IActionResult> TogglePublish([FromRoute] int id, CancellationToken cancellationToken)
		{
			{
				var isUpdated = await _pollService.TogglePublishStatusAsync(id, cancellationToken);

				return isUpdated.IsSuccess ? NoContent() : isUpdated.ToProblem();
			}
		}

	}
}
