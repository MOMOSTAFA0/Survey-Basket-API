namespace Survey_Basket.Authantication.Filters
{
	public class PermissionAuthorizationHandeler : AuthorizationHandler<PermissionRequirment>
	{
		protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirment requirement)
		{

			if (context.User.Identity is not { IsAuthenticated: true } ||
				!context.User.Claims.Any(c => c.Value == requirement.Permission && c.Type == Permissions.Type))
				return;

			context.Succeed(requirement);
			return;
		}
	}
}
