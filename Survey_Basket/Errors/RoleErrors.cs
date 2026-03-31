namespace Survey_Basket.Errors
{
	public static class RoleErrors
	{
		public static readonly Error NOTFound = new("Roles.NotFound", Description: "role is not found within spacific ID", StatusCodes.Status404NotFound);
		public static readonly Error Dublicated = new("Roles.Dublicated", Description: " this roleName has been added before", StatusCodes.Status409Conflict);
		public static readonly Error InvalidPermissions = new("Roles.InvalidPermissions", Description: "you have enteres Invalid Permissions", StatusCodes.Status400BadRequest);
		public static readonly Error InvalidRoles = new("Roles.InvalidRoles", Description: "you have enteres Invalid Roles", StatusCodes.Status400BadRequest);


	}
}
