using Survey_Basket.Contracts.Roles;

namespace Survey_Basket.Services
{
	public interface IRoleService
	{
		Task<IEnumerable<RoleResponse>> GetAllAsync(CancellationToken cancellationToken, bool IncludeDisabled = false);
		Task<Result<RoleDetailResponse>> GetRoleDetailAsync(string RoleID, CancellationToken cancellationToken);
		Task<Result<RoleDetailResponse>> AddRoleAsync(RoleRequest request, CancellationToken cancellationToken);
		Task<Result> UpdateRoleAsync(RoleRequest request, string RoleId, CancellationToken cancellationToken);
		Task<Result> ToggleStatusAsync(string RoleID);
	}
}
