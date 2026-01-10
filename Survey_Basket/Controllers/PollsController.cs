using Mapster;
using Microsoft.AspNetCore.Mvc;
using Survey_Basket.Contracts.Requests;
using Survey_Basket.Contracts.Responses;
using Survey_Basket.Mapping;
using Survey_Basket.Model;
using Survey_Basket.Services;

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
		public IActionResult Getall()
		{
			var polls = _pollService.GetAll();
			return Ok(polls.Adapt<IEnumerable<PollResponse>>());
		}

		[HttpGet("GetByID/{id}")]
		public IActionResult GetByID([FromRoute] int id)
		{
			var poll = _pollService.GetById(id);
			return poll is null? NotFound() : Ok(poll.Adapt<PollResponse>());
		}

		[HttpPost( "Create")]
		public IActionResult CreatePoll([FromBody]CreatePollRequest request)
		{
			var poll=_pollService.CreatPoll(request.Adapt<Poll>());

			return CreatedAtAction(nameof(CreatePoll), new{ID=poll._id},poll);
		}

		[HttpPut("Update/{id}")]
		public IActionResult UpdatePoll([FromRoute]int id,[FromBody] CreatePollRequest request)
		{
			if(_pollService.UpdatePoll(id, request.Adapt<Poll>()))
				return NoContent();
			return NotFound();
		}

		[HttpPost("Delete/{id}")]
		public IActionResult DeletePoll([FromRoute]int id)
		{
			if (_pollService.DeletePoll(id))
				return NoContent();
			return NotFound();
		}

	}
}
