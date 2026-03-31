using Survey_Basket.Contracts.Polls;
using Survey_Basket.Contracts.Requests;

namespace Survey_Basket.Services
{
	public interface IPollService
	{
		Task<Result<IEnumerable<PollResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<Result<IEnumerable<PollResponse>>> GetCurrentAsync(CancellationToken cancellationToken = default);
		Task<Result<PollResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<Result<PollResponse>> CreatPollAsync(PollRequest request, CancellationToken cancellationToken = default);
		Task<Result> UpdatePollAsync(int id, PollRequest request, CancellationToken cancellationToken = default);
		Task<Result> DeletePollAsync(int id, CancellationToken cancellationToken = default);
		Task<Result> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default);
	}
}
