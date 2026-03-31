namespace Survey_Basket.Persistance.EntitiesConfigurations
{
	public class PollConfigurations : IEntityTypeConfiguration<Poll>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Poll> builder)
		{
			builder.HasKey(x => x.Id);
			builder.HasIndex(x => x.Title).IsUnique();
			builder.Property(x => x.Title).HasMaxLength(100);
			builder.Property(X => X.Summary).HasMaxLength(1500);
		}
	}

}
