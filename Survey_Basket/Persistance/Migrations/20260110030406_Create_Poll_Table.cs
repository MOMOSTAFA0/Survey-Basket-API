using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey_Basket.Persistance.Migrations
{
	/// <inheritdoc />
	public partial class Create_Poll_Table : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "polls",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					Summary = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
					IsPublished = table.Column<bool>(type: "bit", nullable: false),
					StartAt = table.Column<DateOnly>(type: "date", nullable: false),
					EndAt = table.Column<DateOnly>(type: "date", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_polls", x => x.Id);
				});

			migrationBuilder.CreateIndex(
				name: "IX_polls_Title",
				table: "polls",
				column: "Title",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "polls");
		}
	}
}
