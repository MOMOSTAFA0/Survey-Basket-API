namespace Survey_Basket.Persistance.EntitiesConfigurations
{
	public class VoteConfigurations : IEntityTypeConfiguration<Vote>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Vote> builder)
		{
			builder.HasIndex(Q => new { Q.PollID, Q.UserID }).IsUnique();
		}
	}

}
