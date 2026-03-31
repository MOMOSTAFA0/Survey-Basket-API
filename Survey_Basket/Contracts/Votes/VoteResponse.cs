namespace Survey_Basket.Contracts.Votes
{
	public record VoteResponse(
		string VoterName,
		DateTime VoteDate,
		IEnumerable<QuestionAnswerResponse> QuestionsAndAnswers
	);
}
