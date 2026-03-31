namespace Survey_Basket.Model
{
	public class Poll : AuditableEntitiy
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Summary { get; set; }
		public bool IsPublished { get; set; }
		public DateOnly StartAt { get; set; }
		public DateOnly EndAt { get; set; }
		public ICollection<Question> Questions { get; set; } = [];
		public ICollection<Vote> Votes { get; set; } = [];

	}
}
