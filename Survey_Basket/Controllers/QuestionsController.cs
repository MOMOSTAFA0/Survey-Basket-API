namespace Survey_Basket.Controllers
{
	[Route("api/polls/{pollId}/[controller]")]
	[ApiController]
	[Authorize]
	public class QuestionsController(IQuestionService questionService) : ControllerBase
	{
		public readonly IQuestionService _questionService = questionService;

		[HttpGet("")]
		//[HasPermissionAttrebuite(Permissions.GetQuestions)]

		public async Task<IActionResult> GetAll([FromRoute] int PollID, [FromQuery] Filters filters, CancellationToken cancellationToken)
		{
			var result = await _questionService.GetAllAsync(PollID, filters, cancellationToken);
			return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
		}

		[HttpGet("AvailableQuestions")]
		[Authorize(Roles = DefaultRole.Member)]
		[EnableRateLimiting(RateLimiterPolicies.ConcurrancyLimit)]

		public async Task<IActionResult> GetAvailableQuestions([FromRoute] int PollID, CancellationToken cancellationToken)
		{
			var CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var result = await _questionService.GetAvailableAsync(PollID, CurrentUserID!, cancellationToken);

			return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
		}

		[HttpGet("{id}")]
		[HasPermissionAttrebuite(Permissions.GetQuestions)]
		public async Task<IActionResult> Get([FromRoute] int PollID, [FromRoute] int id, CancellationToken cancellationToken)
		{
			var result = await _questionService.GetAsync(PollID, id, cancellationToken);
			return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
		}

		[HttpPost("")]
		[HasPermissionAttrebuite(Permissions.AddQuestions)]

		public async Task<IActionResult> CreateQuestion([FromRoute] int pollId, [FromBody] QuestionRequest request, CancellationToken cancellationToken)
		{
			var result = await _questionService.CreateAsync(pollId, request, cancellationToken);
			return result.IsSuccess
				? CreatedAtAction(nameof(CreateQuestion), new { ID = result.Value.Id, PollID = pollId }, result.Value)
				: result.ToProblem();
		}

		[HttpPut("{id}")]
		[HasPermissionAttrebuite(Permissions.UpdateQuestions)]

		public async Task<IActionResult> Update([FromRoute] int PollID, [FromRoute] int id, QuestionRequest request, CancellationToken cancellationToken)
		{
			var result = await _questionService.UpdateAsync(PollID, id, request, cancellationToken);
			return result.IsSuccess ? NoContent() : result.ToProblem();
		}

		[HttpPut("{id}/toggle-status")]
		[HasPermissionAttrebuite(Permissions.UpdateQuestions)]

		public async Task<IActionResult> ToggleStatus([FromRoute] int PollID, [FromRoute] int id, CancellationToken cancellationToken)
		{
			var result = await _questionService.ToggleStatusAsync(PollID, id, cancellationToken);
			return result.IsSuccess ? NoContent() : result.ToProblem();
		}

	}
}
