namespace Survey_Basket.Contracts.Questions
{
	public class QuestionValidator : AbstractValidator<QuestionRequest>
	{
		public QuestionValidator()
		{
			RuleFor(Q => Q.Content).NotEmpty()
								 .Length(3, 1000);
			RuleFor(Q => Q.Content).NotNull();

			RuleFor(Q => Q.Answers).NotEmpty()
								   .Must(Q => Q.Count > 1)
								   .WithMessage("Questions should has at least two answers")
								   .When(Q => Q.Answers != null);

			RuleFor(Q => Q.Answers).Must(Q => Q.Distinct()
								   .Count() == Q.Count)
								   .WithMessage("You can't add duplicated answers for the same question")
								   .When(Q => Q.Answers != null);
		}
	}
}
