using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Survey_Basket.Persistance.EntitiesConfigurations
{
	public class UserRoleConfig : IEntityTypeConfiguration<ApplicationRole>
	{
		public void Configure(EntityTypeBuilder<ApplicationRole> builder)
		{

			builder.HasData([

				new ApplicationRole
				{
					Id=DefaultRole.AdminRoleID,
					Name=DefaultRole.Admin,
					ConcurrencyStamp=DefaultRole.AdminConcurrancyStamp,
					NormalizedName=DefaultRole.Admin.ToUpper()
				},
				new ApplicationRole
				{
					Id=DefaultRole.MemberRoleID,
					Name=DefaultRole.Member,
					ConcurrencyStamp=DefaultRole.MemberConcurrancyStamp,
					NormalizedName=DefaultRole.Member.ToUpper(),
					IsDefault=true
				}
			]);


		}
	}
}
