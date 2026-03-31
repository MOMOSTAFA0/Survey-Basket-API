namespace Survey_Basket.Contracts.Votes
{
	public class VoteAnswerRequestValidator : AbstractValidator<VoteAnswerRequest>
	{
		public VoteAnswerRequestValidator()
		{
			RuleFor(V => V.QuestionId).GreaterThanOrEqualTo(1);
			RuleFor(V => V.AnswerID).GreaterThanOrEqualTo(1);
		}
	}
}
