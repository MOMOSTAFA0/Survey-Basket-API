using Survey_Basket.Contracts.Votes;

namespace Survey_Basket.Contracts.Results
{
	public record PollVotesResponse(
		string Title,
		DateOnly StartAt,
		DateOnly EndeAt,
		IEnumerable<VoteResponse> Votes
	);
}
