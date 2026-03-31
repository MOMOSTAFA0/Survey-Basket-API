using Survey_Basket.Contracts.Polls;
using Survey_Basket.Contracts.Requests;

namespace Survey_Basket.Services
{
	public class PollService : IPollService
	{
		private readonly ApplicationDbContext _context;
		private readonly INotefecationService _notefecationService;

		public PollService(ApplicationDbContext context, INotefecationService notefecationService)
		{
			_context = context;
			_notefecationService = notefecationService;
		}

		public async Task<Result<PollResponse>> CreatPollAsync(PollRequest request, CancellationToken cancellationToken = default)
		{
			var IsExisting = await _context.polls.AnyAsync(x => x.Title == request.Title);
			if (IsExisting)
				return Result.Failure<PollResponse>(PollsErrors.DublicatedTitle);

			await _context.polls.AddAsync(request.Adapt<Poll>(), cancellationToken);
			var effectedRows = await _context.SaveChangesAsync(cancellationToken);
			return effectedRows > 0
				? Result.Success(request.Adapt<PollResponse>()) : Result.Failure<PollResponse>(PollsErrors.InvalidData);
		}

		public async Task<Result> DeletePollAsync(int id, CancellationToken cancellationToken = default)
		{
			var poll = await _context.polls.FindAsync(id, cancellationToken);
			if (poll is null)
				return Result.Failure(PollsErrors.NotFound);

			_context.Remove(poll);
			await _context.SaveChangesAsync(cancellationToken);
			return Result.Success();
		}

		public async Task<Result<IEnumerable<PollResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			var polls = await _context.polls.AsNoTracking()
										   .Select(P => new PollResponse(P.Id, P.Title, P.Summary, P.IsPublished, P.StartAt, P.EndAt))
										   .ToListAsync(cancellationToken);

			return polls is not null
				? Result.Success<IEnumerable<PollResponse>>(polls)
				: Result.Failure<IEnumerable<PollResponse>>(PollsErrors.Empty);
		}

		public async Task<Result<IEnumerable<PollResponse>>> GetCurrentAsync(CancellationToken cancellationToken = default)
		{
			var polls = await _context.polls.Where(P => P.IsPublished == true
														&& P.StartAt <= DateOnly.FromDateTime(DateTime.UtcNow)
														&& P.EndAt >= DateOnly.FromDateTime(DateTime.UtcNow))
										   .AsNoTracking()
										   .Select(P => new PollResponse(P.Id, P.Title, P.Summary, P.IsPublished, P.StartAt, P.EndAt))
										   .ToListAsync(cancellationToken);

			return polls is not null
				? Result.Success<IEnumerable<PollResponse>>(polls)
				: Result.Failure<IEnumerable<PollResponse>>(PollsErrors.Empty);
		}
		public async Task<Result<PollResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			var poll = await _context.polls.FindAsync(id, cancellationToken);
			return poll is not null
				? Result.Success(poll.Adapt<PollResponse>())
				: Result.Failure<PollResponse>(PollsErrors.NotFound);
		}

		public async Task<Result> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default)
		{
			var poll = await _context.polls.FindAsync(id, cancellationToken);
			if (poll is null)
				return Result.Failure(PollsErrors.NotFound);
			poll.IsPublished = !poll.IsPublished;
			await _context.SaveChangesAsync(cancellationToken);

			if (poll.IsPublished)
			{
				BackgroundJob.Enqueue(() => _notefecationService.SendPollNotificationsAsync(poll.Id));
			}
			return Result.Success();
		}

		public async Task<Result> UpdatePollAsync(int id, PollRequest request, CancellationToken cancellationToken = default)
		{
			var currentPoll = await _context.polls.FindAsync(id, cancellationToken);
			if (currentPoll is null)
				return Result.Failure(PollsErrors.NotFound);

			var isDuplicatedTitle = await _context.polls.AnyAsync(x => x.Title == request.Title && x.Id != id, cancellationToken);
			if (isDuplicatedTitle)
				return Result.Failure<PollResponse>(PollsErrors.DublicatedTitle);

			currentPoll = request.Adapt(currentPoll);
			await _context.SaveChangesAsync(cancellationToken);
			return Result.Success();
		}

	}
}
