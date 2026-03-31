using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey_Basket.Persistance.Migrations
{
	/// <inheritdoc />
	public partial class vv : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "019c8b0a-4cbc-79ce-857f-1a932aff1611",
				column: "PasswordHash",
				value: "AQAAAAIAAYagAAAAEJtqiYU5rpwgqAQ+hHqcoGrhoFiHpGdYskODdMb8VTYFgiJiSh0SKSbzD2SHikaQNw==");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "019c8b0a-4cbc-79ce-857f-1a932aff1611",
				column: "PasswordHash",
				value: "AQAAAAIAAYagAAAAEBLcPMl6DC77HKel1ka2rdR+P5v9HfMnxRVvu35/wAirtI/wmI8Ydh6XURoSVVzwsw==");
		}
	}
}
