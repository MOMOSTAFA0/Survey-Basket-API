using Survey_Basket.Model;

namespace Survey_Basket.Services
{
	public interface IPollService
	{
		IEnumerable<Poll> GetAll();
		Poll GetById(int id);
		Poll CreatPoll(Poll poll);
		bool UpdatePoll(int id,Poll poll);
		bool DeletePoll(int id);
	}
}
