namespace Survey_Basket.Model
{
	public class Answer : AuditableEntitiy
	{
		public int Id { get; set; }
		public string Content { get; set; } = string.Empty;
		public bool IsActive { get; set; } = true;
		public int QuestionID { get; set; }
		public Question Question { get; set; } = default!;
	}
}
