using Survey_Basket.Contracts.Roles;

namespace Survey_Basket.Services
{
	public class RoleService(RoleManager<ApplicationRole> roleManager, ApplicationDbContext context) : IRoleService
	{
		private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
		private readonly ApplicationDbContext _context = context;

		public async Task<IEnumerable<RoleResponse>> GetAllAsync(CancellationToken cancellationToken, bool IncludeDisabled = false) =>
			await _roleManager.Roles
			.Where(R => !R.IsDefault && (!R.IsDeleted || IncludeDisabled))
			.Select(R => new RoleResponse(R.Id, R.Name!, R.IsDeleted))
			.AsNoTracking()
			.ToListAsync(cancellationToken);
		public async Task<Result<RoleDetailResponse>> GetRoleDetailAsync(string RoleID, CancellationToken cancellationToken)
		{

			if (await _roleManager.FindByIdAsync(RoleID) is not { } role)
				return Result.Failure<RoleDetailResponse>(RoleErrors.NOTFound);

			var RoleClaims = await _roleManager.GetClaimsAsync(role);

			return Result.Success(new RoleDetailResponse(role.Id, role.Name!, role.IsDeleted, RoleClaims.Select(rc => rc.Value)));
		}

		public async Task<Result<RoleDetailResponse>> AddRoleAsync(RoleRequest request, CancellationToken cancellationToken)
		{
			var RoleIsExisted = await _roleManager.RoleExistsAsync(request.RoleName);
			if (RoleIsExisted)
				return Result.Failure<RoleDetailResponse>(RoleErrors.Dublicated);
			var AllowedPermissions = Permissions.GetAllPermissions();
			if (request.Permissions.Except(AllowedPermissions).Any())
				return Result.Failure<RoleDetailResponse>(RoleErrors.InvalidPermissions);

			var Role = new ApplicationRole
			{
				Name = request.RoleName,
				ConcurrencyStamp = new Guid().ToString(),
			};

			var AddingRoleResult = await _roleManager.CreateAsync(Role);
			if (AddingRoleResult.Succeeded)
			{
				var claims = request.Permissions.Select(P => new IdentityRoleClaim<string>
				{
					RoleId = Role.Id,
					ClaimType = Permissions.Type,
					ClaimValue = P
				});
				await _context.AddRangeAsync(claims);
				await _context.SaveChangesAsync(cancellationToken);
				return Result.Success(new RoleDetailResponse(Role.Id, Role.Name, Role.IsDeleted, request.Permissions));

			}
			var Error = AddingRoleResult.Errors.First();
			return Result.Failure<RoleDetailResponse>(new Error(Error.Code, Error.Description, StatusCodes.Status400BadRequest));
		}
		public async Task<Result> UpdateRoleAsync(RoleRequest request, string RoleId, CancellationToken cancellationToken)
		{
			if (await _roleManager.FindByIdAsync(RoleId) is not { } Role)
				return Result.Failure(RoleErrors.NOTFound);

			var roleNameExisted = _roleManager.Roles.Any(R => R.Name == request.RoleName && R.Id != RoleId);

			if (roleNameExisted)
				return Result.Failure(RoleErrors.Dublicated);


			Role.Name = request.RoleName;
			var updateResult = await _roleManager.UpdateAsync(Role);

			if (updateResult.Succeeded)
			{
				var Claims = (await _roleManager.GetClaimsAsync(Role)).Select(c => c.Value).ToList();
				var AllowedPermissions = Permissions.GetAllPermissions();

				if (request.Permissions.Except(AllowedPermissions).Any())
					return Result.Failure(RoleErrors.InvalidPermissions);


				var newPermissions = request.Permissions
					.Except(Claims)
					.Select(C => new IdentityRoleClaim<string>
					{
						RoleId = RoleId,
						ClaimValue = C,
						ClaimType = Permissions.Type
					});

				var RemovedPermissions = Claims.Except(request.Permissions);

				await _context.RoleClaims
					.Where(x => x.RoleId == RoleId && RemovedPermissions.Contains(x.ClaimValue))
					.ExecuteDeleteAsync();
				await _context.AddRangeAsync(newPermissions);
				_context.SaveChanges();
				return Result.Success();
			}

			var Error = updateResult.Errors.First();
			return Result.Failure<RoleDetailResponse>(new Error(Error.Code, Error.Description, StatusCodes.Status400BadRequest));
		}

		public async Task<Result> ToggleStatusAsync(string RoleID)
		{
			if (await _roleManager.FindByIdAsync(RoleID) is not { } Role)
			{
				return Result.Failure(RoleErrors.NOTFound);
			}
			Role.IsDeleted = !Role.IsDeleted;
			var UpdateResult = await _roleManager.UpdateAsync(Role);
			if (!UpdateResult.Succeeded)
			{
				var Error = UpdateResult.Errors.First();
				return Result.Failure<RoleDetailResponse>(new Error(Error.Code, Error.Description, StatusCodes.Status400BadRequest));

			}
			return Result.Success();
		}
	}
}
