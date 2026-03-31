namespace Survey_Basket.Contracts.Votes
{
	public record VotesPerDayResponse(
		string Title,
		DateOnly Date,
		int NumberOfVotes
	);

}
