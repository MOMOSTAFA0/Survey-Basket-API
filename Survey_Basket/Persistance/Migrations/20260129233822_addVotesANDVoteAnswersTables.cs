using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey_Basket.Persistance.Migrations
{
	/// <inheritdoc />
	public partial class addVotesANDVoteAnswersTables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "votes",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					PollID = table.Column<int>(type: "int", nullable: false),
					UserID = table.Column<string>(type: "nvarchar(450)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_votes", x => x.Id);
					table.ForeignKey(
						name: "FK_votes_AspNetUsers_UserID",
						column: x => x.UserID,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_votes_polls_PollID",
						column: x => x.PollID,
						principalTable: "polls",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "votesAnswer",
				columns: table => new
				{
					id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					VoteId = table.Column<int>(type: "int", nullable: false),
					QuestionId = table.Column<int>(type: "int", nullable: false),
					AnswerId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_votesAnswer", x => x.id);
					table.ForeignKey(
						name: "FK_votesAnswer_Answers_AnswerId",
						column: x => x.AnswerId,
						principalTable: "Answers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_votesAnswer_Questions_QuestionId",
						column: x => x.QuestionId,
						principalTable: "Questions",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
					table.ForeignKey(
						name: "FK_votesAnswer_votes_VoteId",
						column: x => x.VoteId,
						principalTable: "votes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_votes_PollID_UserID",
				table: "votes",
				columns: new[] { "PollID", "UserID" },
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_votes_UserID",
				table: "votes",
				column: "UserID");

			migrationBuilder.CreateIndex(
				name: "IX_votesAnswer_AnswerId",
				table: "votesAnswer",
				column: "AnswerId");

			migrationBuilder.CreateIndex(
				name: "IX_votesAnswer_QuestionId_VoteId",
				table: "votesAnswer",
				columns: new[] { "QuestionId", "VoteId" },
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_votesAnswer_VoteId",
				table: "votesAnswer",
				column: "VoteId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "votesAnswer");

			migrationBuilder.DropTable(
				name: "votes");
		}
	}
}
