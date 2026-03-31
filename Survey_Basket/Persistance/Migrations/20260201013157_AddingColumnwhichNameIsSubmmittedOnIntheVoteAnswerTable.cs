using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey_Basket.Persistance.Migrations
{
	/// <inheritdoc />
	public partial class AddingColumnwhichNameIsSubmmittedOnIntheVoteAnswerTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<DateTime>(
				name: "SubmittedOn",
				table: "votes",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "SubmittedOn",
				table: "votes");
		}
	}
}
