namespace Survey_Basket.Model
{
	public class ApplicationRole : IdentityRole
	{
		public bool IsDefault { get; set; }
		public bool IsDeleted { get; set; }
	}
}
