namespace Survey_Basket.Services
{
	public interface INotefecationService
	{
		Task SendPollNotificationsAsync(int? PollId = null);
	}
}
