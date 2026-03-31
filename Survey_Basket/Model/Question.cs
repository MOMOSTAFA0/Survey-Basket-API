namespace Survey_Basket.Model
{
	public class Question : AuditableEntitiy
	{
		public int Id { get; set; }
		public string Content { get; set; } = string.Empty;
		public int PollID { get; set; }
		public Poll Poll { get; set; } = default!;
		public bool IsActive { get; set; } = true;
		public ICollection<Answer> Answers { get; set; } = [];
		public ICollection<VoteAnswer> VoteAnswers { get; set; } = [];

	}
}
