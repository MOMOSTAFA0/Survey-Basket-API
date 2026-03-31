using Survey_Basket.Contracts.Requests;

namespace Survey_Basket.Contracts.Polls
{
	public class PollRequesValidator : AbstractValidator<PollRequest>
	{
		public PollRequesValidator()
		{
			RuleFor(x => x.Title)
				.Length(3, 100)
				.When(x => x.Title != null)
				.WithMessage("{PropertyName} is invalid, title length should be between 5 and 50 charcaters");

			RuleFor(x => x.Summary).NotEmpty().Length(3, 1500).WithMessage("{PropertyName} ,Description is requeird");

			RuleFor(x => x.StartAt)
				.NotEmpty()
				.GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today));
			RuleFor(x => x.EndAt)
				.NotEmpty();
			RuleFor(x => x)
				.Must(HasValidDate)
				.WithName(nameof(PollRequest.EndAt))
				.WithMessage("{PropertyName} must be greater than or equale startdate");


		}
		private bool HasValidDate(PollRequest pollRequest) => pollRequest.EndAt >= pollRequest.StartAt;

	}
}
