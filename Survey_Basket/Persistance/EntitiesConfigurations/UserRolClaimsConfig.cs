using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Survey_Basket.Persistance.EntitiesConfigurations
{
	public class UserRolClaimsConfig : IEntityTypeConfiguration<IdentityRoleClaim<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
		{
			var AllPermissions = Permissions.GetAllPermissions();

			var AdminClaims = new List<IdentityRoleClaim<string>>();

			for (int i = 0; i < AllPermissions.Count; i++)
			{
				AdminClaims.Add(new IdentityRoleClaim<string>
				{
					Id = i + 1,
					ClaimType = Permissions.Type,
					ClaimValue = AllPermissions[i],
					RoleId = DefaultRole.AdminRoleID,
				});
			}
			builder.HasData(AdminClaims);
		}
	}
}
