namespace Survey_Basket.Contracts.Roles
{
	public record RoleDetailResponse(
		string ID,
		string RoleName,
		bool isDeleted,
		IEnumerable<string> Permissions
	);
}
