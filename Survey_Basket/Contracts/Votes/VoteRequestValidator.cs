namespace Survey_Basket.Contracts.Votes
{
	public class VoteRequestValidator : AbstractValidator<VoteRequest>
	{
		public VoteRequestValidator()
		{
			RuleFor(V => V.Answers).NotEmpty();
			RuleForEach(V => V.Answers)
				.SetInheritanceValidator(V =>
					 V.Add(new VoteAnswerRequestValidator()));
		}
	}
}
