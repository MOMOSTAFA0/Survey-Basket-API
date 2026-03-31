namespace Survey_Basket.Contracts.Votes
{
	public record VotesPerQuestionResponse(
		string PollTitle,
		string Question,
		IEnumerable<VotesPerAnswerResponse> SelectedAnswers
	);

}
