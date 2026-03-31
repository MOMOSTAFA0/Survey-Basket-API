using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey_Basket.Persistance.Migrations
{
	/// <inheritdoc />
	public partial class AddQuestionsAndAnswersTables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_polls_AspNetUsers_CreatedById",
				table: "polls");

			migrationBuilder.CreateTable(
				name: "Questions",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
					PollID = table.Column<int>(type: "int", nullable: false),
					IsActive = table.Column<bool>(type: "bit", nullable: false),
					CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
					CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
					UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
					UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Questions", x => x.Id);
					table.ForeignKey(
						name: "FK_Questions_AspNetUsers_CreatedById",
						column: x => x.CreatedById,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Questions_AspNetUsers_UpdatedById",
						column: x => x.UpdatedById,
						principalTable: "AspNetUsers",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_Questions_polls_PollID",
						column: x => x.PollID,
						principalTable: "polls",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Answers",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
					IsActive = table.Column<bool>(type: "bit", nullable: false),
					QuestionID = table.Column<int>(type: "int", nullable: false),
					CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
					CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
					UpdatedyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
					UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
					UpdatedById = table.Column<string>(type: "nvarchar(450)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Answers", x => x.Id);
					table.ForeignKey(
						name: "FK_Answers_AspNetUsers_CreatedById",
						column: x => x.CreatedById,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_Answers_AspNetUsers_UpdatedById",
						column: x => x.UpdatedById,
						principalTable: "AspNetUsers",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_Answers_Questions_QuestionID",
						column: x => x.QuestionID,
						principalTable: "Questions",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Answers_CreatedById",
				table: "Answers",
				column: "CreatedById");

			migrationBuilder.CreateIndex(
				name: "IX_Answers_QuestionID_Content",
				table: "Answers",
				columns: new[] { "QuestionID", "Content" },
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Answers_UpdatedById",
				table: "Answers",
				column: "UpdatedById");

			migrationBuilder.CreateIndex(
				name: "IX_Questions_CreatedById",
				table: "Questions",
				column: "CreatedById");

			migrationBuilder.CreateIndex(
				name: "IX_Questions_PollID_Content",
				table: "Questions",
				columns: new[] { "PollID", "Content" },
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Questions_UpdatedById",
				table: "Questions",
				column: "UpdatedById");

			migrationBuilder.AddForeignKey(
				name: "FK_polls_AspNetUsers_CreatedById",
				table: "polls",
				column: "CreatedById",
				principalTable: "AspNetUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_polls_AspNetUsers_CreatedById",
				table: "polls");

			migrationBuilder.DropTable(
				name: "Answers");

			migrationBuilder.DropTable(
				name: "Questions");

			migrationBuilder.AddForeignKey(
				name: "FK_polls_AspNetUsers_CreatedById",
				table: "polls",
				column: "CreatedById",
				principalTable: "AspNetUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}
	}
}
