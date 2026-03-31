using Survey_Basket.Contracts.Results;
using Survey_Basket.Contracts.Votes;

namespace Survey_Basket.Services
{
	public class ResultService(ApplicationDbContext context) : IResultService
	{
		private readonly ApplicationDbContext _context = context;

		public async Task<Result<PollVotesResponse>> GetPollVotesAsync(int PollID, CancellationToken cancellationToken)
		{
			var PollExists = await _context.polls.AnyAsync(P => P.Id == PollID);
			if (!PollExists)
				return Result.Failure<PollVotesResponse>(PollsErrors.NotFound);
			var pollVotes = await _context.polls
				.AsSplitQuery()
				.Where(P => P.Id == PollID)
				.Select(P => new PollVotesResponse(
					P.Title,
					P.StartAt,
					P.EndAt,
					P.Votes.Select(V => new VoteResponse(
						$"{V.User.FirstName} {V.User.LastName}",
						V.SubmittedOn,
						V.VotesAnswers.Select(VA => new QuestionAnswerResponse(
							VA.Question.Content,
							VA.Answer.Content
						))
					))
				)).SingleOrDefaultAsync(cancellationToken);
			return pollVotes == null
				? Result.Failure<PollVotesResponse>(PollsErrors.NotFound)
				: Result.Success(pollVotes);
		}
		public async Task<Result<IEnumerable<VotesPerDayResponse>>> GetVotesPerDayAsync(int PollID, CancellationToken cancellationToken)
		{
			var PollTitle = await _context.polls.Where(P => P.Id == PollID).Select(P => P.Title).SingleOrDefaultAsync(cancellationToken);

			if (PollTitle == null)
				return Result.Failure<IEnumerable<VotesPerDayResponse>>(PollsErrors.NotFound);

			var VotesPerDay = await _context.votes
				.Where(V => V.PollID == PollID)
				.GroupBy(V => new { Date = DateOnly.FromDateTime(V.SubmittedOn) })
				.Select(G => new VotesPerDayResponse(PollTitle,
					G.Key.Date,
					G.Count()
				)).ToListAsync(cancellationToken);
			return Result.Success<IEnumerable<VotesPerDayResponse>>(VotesPerDay);
		}
		public async Task<Result<IEnumerable<VotesPerQuestionResponse>>> GetVotesPerQuestionAsync(int PollID, CancellationToken cancellationToken)
		{
			var PollTitle = await _context.polls.Where(P => P.Id == PollID).Select(P => P.Title).SingleOrDefaultAsync(cancellationToken);

			if (PollTitle == null)
				return Result.Failure<IEnumerable<VotesPerQuestionResponse>>(PollsErrors.NotFound);
			var VotesPerQuestion = await _context.votesAnswer
				.Where(VA => VA.vote.PollID == PollID)
				.Select(VA => new VotesPerQuestionResponse(
					PollTitle,
					VA.Question.Content,
					VA.Question.VoteAnswers
					.GroupBy(VA => new { AnswerId = VA.Answer.Id, AnswerContent = VA.Answer.Content })
					.Select(G => new VotesPerAnswerResponse(
						G.Key.AnswerContent,
						G.Count()
					))
				)).ToListAsync(cancellationToken);
			return Result.Success<IEnumerable<VotesPerQuestionResponse>>(VotesPerQuestion);
		}
	}
}
