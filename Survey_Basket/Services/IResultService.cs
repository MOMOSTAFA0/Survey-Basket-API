using Survey_Basket.Contracts.Results;
using Survey_Basket.Contracts.Votes;

namespace Survey_Basket.Services
{
	public interface IResultService
	{
		public Task<Result<PollVotesResponse>> GetPollVotesAsync(int PollID, CancellationToken cancellationToken);
		public Task<Result<IEnumerable<VotesPerDayResponse>>> GetVotesPerDayAsync(int PollID, CancellationToken cancellationToken);
		public Task<Result<IEnumerable<VotesPerQuestionResponse>>> GetVotesPerQuestionAsync(int PollID, CancellationToken cancellationToken);


	}
}
