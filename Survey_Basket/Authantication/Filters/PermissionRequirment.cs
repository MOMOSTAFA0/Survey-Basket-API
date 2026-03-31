namespace Survey_Basket.Authantication.Filters
{
	public class PermissionRequirment(string Permission) : IAuthorizationRequirement
	{
		public string Permission { get; } = Permission;
	}
}
