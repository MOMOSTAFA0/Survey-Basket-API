using FluentValidation;
using Survey_Basket.Contracts.Requests;

namespace Survey_Basket.Contracts.Validations
{
	public class CreatePollRequesValidator:AbstractValidator<CreatePollRequest>
	{
		public CreatePollRequesValidator()
		{
			RuleFor(x => x._title)
				.Length(5, 50)
				.When(x=>x._title!=null)
				.WithMessage("{PropertyName} is invalid, title length should be between 5 and 50 charcaters");

			RuleFor(x => x._discreotion).NotEmpty().WithMessage("{PropertyName} ,Description is requeird");
		}
	}
}
