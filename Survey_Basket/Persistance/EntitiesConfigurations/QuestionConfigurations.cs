namespace Survey_Basket.Persistance.EntitiesConfigurations
{
	public class QuestionConfigurations : IEntityTypeConfiguration<Question>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Question> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(Q => Q.Content).HasMaxLength(1000);
			builder.HasIndex(Q => new { Q.PollID, Q.Content }).IsUnique();
		}
	}

}
