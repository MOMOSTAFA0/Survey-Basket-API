namespace Survey_Basket.Contracts.Roles
{
	public record class RoleRequest(
		string RoleName,
		IEnumerable<string> Permissions
	);
}
