namespace Survey_Basket.Persistance.EntitiesConfigurations
{
	public class VoteAnswerConfigurations : IEntityTypeConfiguration<VoteAnswer>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<VoteAnswer> builder)
		{
			builder.HasIndex(Q => new { Q.QuestionId, Q.VoteId }).IsUnique();
		}
	}

}
