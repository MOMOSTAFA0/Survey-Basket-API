namespace Survey_Basket.Persistance.EntitiesConfigurations
{
	public class AnswersConfigurations : IEntityTypeConfiguration<Answer>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Answer> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(Q => Q.Content).HasMaxLength(1000);
			builder.HasIndex(Q => new { Q.QuestionID, Q.Content }).IsUnique();
		}
	}

}
