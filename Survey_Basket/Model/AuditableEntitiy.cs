namespace Survey_Basket.Model
{
	public class AuditableEntitiy
	{
		public string CreatedById { get; set; } = string.Empty;
		public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
		public string? UpdatedyId { get; set; } = string.Empty;
		public DateTime? UpdatedOn { get; set; }
		public ApplicationUser CreatedBy { get; set; } = default!;
		public ApplicationUser? UpdatedBy { get; set; }

	}
}
