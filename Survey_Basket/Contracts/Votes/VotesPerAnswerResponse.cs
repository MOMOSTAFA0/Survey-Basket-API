namespace Survey_Basket.Contracts.Votes
{
	public record VotesPerAnswerResponse(
		string Answer,
		int Count
	);
}
