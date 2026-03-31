namespace Survey_Basket.Mapping
{
	public class QuestionMapping : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<QuestionRequest, Question>()
				   .Map(dest => dest.Answers, src => src.Answers.Select(answer => new Answer { Content = answer }).ToList());
		}
	}
}
