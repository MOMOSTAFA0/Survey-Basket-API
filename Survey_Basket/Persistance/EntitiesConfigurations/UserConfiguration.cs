using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Survey_Basket.Persistance.EntitiesConfigurations
{
	public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.OwnsMany(U => U.RefreshTokens)
				   .ToTable("RefreshTokens")
				   .WithOwner()
				   .HasForeignKey("UserId");

			builder.Property(x => x.FirstName).HasMaxLength(100);
			builder.Property(X => X.LastName).HasMaxLength(100);

			builder.HasData(new ApplicationUser
			{
				Id = DefaultUsers.AdminID,
				FirstName = "Survey-Basket",
				LastName = "Admin",
				Email = DefaultUsers.AdminEmail,
				SecurityStamp = DefaultUsers.AdminSecurityStamp.ToUpper(),
				ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
				UserName = DefaultUsers.AdminEmail,
				NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
				NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
				EmailConfirmed = true,
				PasswordHash = DefaultUsers.AdminPasswordHash
			});
		}
	}
}