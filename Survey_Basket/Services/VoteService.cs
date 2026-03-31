using Survey_Basket.Contracts.Votes;

namespace Survey_Basket.Services
{
	public class VoteService(ApplicationDbContext Context) : IVoteService
	{
		private readonly ApplicationDbContext _context = Context;

		public async Task<Result> CreateAsync(int PollID, string UserID, VoteRequest voteRequest, CancellationToken cancellationToken)
		{
			var HasVote = await _context.votes.AnyAsync(V => V.PollID == PollID && V.UserID == UserID, cancellationToken);
			if (HasVote)
				return Result.Failure(VoteErrors.DublicatedVote);

			var pollIsExisted = await _context.polls.AnyAsync(P => P.Id == PollID
																   && P.IsPublished
																   && P.StartAt <= DateOnly.FromDateTime(DateTime.UtcNow)
																   && P.EndAt >= DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken);
			if (!pollIsExisted)
				return Result.Failure(PollsErrors.NotFound);
			var AvaliableQuestions = await _context.Questions.Where(Q => Q.PollID == PollID && Q.IsActive)
															 .Select(Q => Q.Id)
															 .ToListAsync(cancellationToken);
			if (!voteRequest.Answers.Select(A => A.QuestionId).SequenceEqual(AvaliableQuestions))
				return Result.Failure(VoteErrors.InvalidQuestions);
			var Vote = new Vote
			{
				PollID = PollID,
				UserID = UserID,
				VotesAnswers = voteRequest.Answers.Select(A => new VoteAnswer { AnswerId = A.AnswerID, QuestionId = A.QuestionId }).ToList()
			};
			await _context.votes.AddAsync(Vote, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
			return Result.Success();
		}
	}
}
