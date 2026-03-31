using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Survey_Basket.Persistance.EntitiesConfigurations
{
	public class UserRolConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
	{
		public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
		{
			builder.HasData(new IdentityUserRole<string>
			{
				UserId = DefaultUsers.AdminID,
				RoleId = DefaultRole.AdminRoleID,
			});
		}
	}
}
