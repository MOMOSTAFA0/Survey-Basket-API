namespace Survey_Basket.Controllers
{
	[Route("api/Polls/{PollID}/[controller]")]
	[ApiController]
	[HasPermissionAttrebuite(Permissions.GetResults)]
	public class ResultsController(IResultService resultService) : ControllerBase
	{
		private readonly IResultService _resultService = resultService;

		[HttpGet("row-dat")]
		public async Task<IActionResult> PollVotes([FromRoute] int PollID, CancellationToken cancellationToken)
		{
			var result = await _resultService.GetPollVotesAsync(PollID, cancellationToken);
			return result.IsSuccess ? Ok(result) : result.ToProblem();

		}
		[HttpGet("votes-per-day")]
		public async Task<IActionResult> VotesPerDay([FromRoute] int PollID, CancellationToken cancellationToken)
		{
			var result = await _resultService.GetVotesPerDayAsync(PollID, cancellationToken);
			return result.IsSuccess ? Ok(result) : result.ToProblem();
		}
		[HttpGet("votes-per-question")]
		public async Task<IActionResult> VotesPerQuestion([FromRoute] int PollID, CancellationToken cancellationToken)
		{
			var result = await _resultService.GetVotesPerQuestionAsync(PollID, cancellationToken);
			return result.IsSuccess ? Ok(result) : result.ToProblem();
		}
	}
}
