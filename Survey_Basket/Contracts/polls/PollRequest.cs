
namespace Survey_Basket.Contracts.Requests
{
	public class PollRequest
	{
		public string Title { get; set; }
		public string Summary { get; set; }
		public bool IsPublished { get; set; }
		public DateOnly StartAt { get; set; }
		public DateOnly EndAt { get; set; }

	}
}
