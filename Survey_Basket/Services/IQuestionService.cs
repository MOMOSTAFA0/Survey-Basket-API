namespace Survey_Basket.Services
{
	public interface IQuestionService
	{
		Task<Result<QuestionResponse>> CreateAsync(int PollID, QuestionRequest request, CancellationToken cancellationToke);
		Task<Result<PagginatedList<QuestionResponse>>> GetAllAsync(int PollID, Filters filters, CancellationToken cancellationToken);
		Task<Result<IEnumerable<QuestionResponse>>> GetAvailableAsync(int PollID, string UserID, CancellationToken cancellationToken);
		Task<Result<QuestionResponse>> GetAsync(int PollID, int QuestionID, CancellationToken cancellationToken);
		Task<Result> UpdateAsync(int PollID, int QuestionID, QuestionRequest request, CancellationToken cancellationToken);
		Task<Result> ToggleStatusAsync(int PollID, int QuestionID, CancellationToken cancellationToken);
	}
}
