using Survey_Basket.Contracts.Roles;

namespace Survey_Basket.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	//[HasPermissionAttrebuite(Permissions.GetRoles)]
	[Authorize(Roles = DefaultRole.Admin)]
	public class RolesController(IRoleService roleService) : ControllerBase
	{
		private readonly IRoleService _roleService = roleService;

		[HttpGet("")]
		public async Task<IActionResult> GetAll([FromQuery] bool InculedDeisabled, CancellationToken cancellationToken)
		{
			var Roles = await _roleService.GetAllAsync(cancellationToken, InculedDeisabled);
			return Ok(Roles);

		}
		[HttpGet("role-detail")]
		public async Task<IActionResult> GetReoleDetail([FromQuery] string RoleID, CancellationToken cancellationToken)
		{
			var RolesResult = await _roleService.GetRoleDetailAsync(RoleID, cancellationToken);
			return RolesResult.IsSuccess ? Ok(RolesResult.Value) : RolesResult.ToProblem();
		}
		[HttpPost("")]
		public async Task<IActionResult> AddRole([FromBody] RoleRequest request, CancellationToken cancellationToken)
		{
			var AddingRolesResult = await _roleService.AddRoleAsync(request, cancellationToken);
			return AddingRolesResult.IsSuccess ? Ok(AddingRolesResult.Value) : AddingRolesResult.ToProblem();
		}
		[HttpPut("")]
		public async Task<IActionResult> UpdateRole([FromBody] RoleRequest request, [FromQuery] string RoleID, CancellationToken cancellationToken)
		{
			var AddingRolesResult = await _roleService.UpdateRoleAsync(request, RoleID, cancellationToken);
			return AddingRolesResult.IsSuccess ? NoContent() : AddingRolesResult.ToProblem();
		}
		[HttpPut("toggle-role-status")]
		public async Task<IActionResult> ToggleRoleStatus([FromQuery] string RoleID, CancellationToken cancellationToken)
		{
			var ToggleRolesResult = await _roleService.ToggleStatusAsync(RoleID);
			return ToggleRolesResult.IsSuccess ? NoContent() : ToggleRolesResult.ToProblem();
		}
	}
}
