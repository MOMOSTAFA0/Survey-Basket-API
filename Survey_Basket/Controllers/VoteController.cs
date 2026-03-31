using Survey_Basket.Contracts.Votes;

namespace Survey_Basket.Controllers
{
	[Route("api/Polls/{PollId}/[controller]")]
	[ApiController]
	[Authorize(Roles = DefaultRole.Member)]
	[EnableRateLimiting(RateLimiterPolicies.ConcurrancyLimit)]
	public class VoteController(IVoteService voteService) : ControllerBase
	{
		private readonly IVoteService _voteService = voteService;

		[HttpPost("")]
		public async Task<IActionResult> Vote([FromRoute] int PollId, VoteRequest request, CancellationToken cancellationToken)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _voteService.CreateAsync(PollId, userId!, request, cancellationToken);
			return result.IsSuccess ? Ok() : result.ToProblem();
		}
	}
}
