namespace Survey_Basket.Model
{
	public sealed class Vote
	{
		public int Id { get; set; }
		public int PollID { get; set; }
		public string UserID { get; set; } = string.Empty;
		public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;
		public ApplicationUser User { get; set; } = default!;
		public Poll Poll { get; set; } = default!;
		public ICollection<VoteAnswer> VotesAnswers { get; set; } = [];

	}
}
