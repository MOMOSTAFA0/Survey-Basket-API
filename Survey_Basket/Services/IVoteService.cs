using Survey_Basket.Contracts.Votes;

namespace Survey_Basket.Services
{
	public interface IVoteService
	{
		Task<Result> CreateAsync(int PollID, string UserID, VoteRequest voteRequest, CancellationToken cancellationToken);
	}
}
