using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Survey_Basket.Persistance.Migrations
{
	/// <inheritdoc />
	public partial class AddIsDesabledColumnToUsersTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<bool>(
				name: "IsDesabled",
				table: "AspNetUsers",
				type: "bit",
				nullable: false,
				defaultValue: false);

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 1,
				column: "ClaimType",
				value: "permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 2,
				column: "ClaimType",
				value: "permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 3,
				column: "ClaimType",
				value: "permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 4,
				column: "ClaimType",
				value: "permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 5,
				column: "ClaimType",
				value: "permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 6,
				column: "ClaimType",
				value: "permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 7,
				column: "ClaimType",
				value: "permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 8,
				column: "ClaimType",
				value: "permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 9,
				column: "ClaimType",
				value: "permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 10,
				column: "ClaimType",
				value: "permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 11,
				column: "ClaimType",
				value: "permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 12,
				column: "ClaimType",
				value: "permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 13,
				column: "ClaimType",
				value: "permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 14,
				column: "ClaimType",
				value: "permissions");

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "019c8b0a-4cbc-79ce-857f-1a932aff1611",
				columns: new[] { "IsDesabled", "PasswordHash" },
				values: new object[] { false, "AQAAAAIAAYagAAAAEBLcPMl6DC77HKel1ka2rdR+P5v9HfMnxRVvu35/wAirtI/wmI8Ydh6XURoSVVzwsw==" });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "IsDesabled",
				table: "AspNetUsers");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 1,
				column: "ClaimType",
				value: "Permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 2,
				column: "ClaimType",
				value: "Permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 3,
				column: "ClaimType",
				value: "Permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 4,
				column: "ClaimType",
				value: "Permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 5,
				column: "ClaimType",
				value: "Permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 6,
				column: "ClaimType",
				value: "Permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 7,
				column: "ClaimType",
				value: "Permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 8,
				column: "ClaimType",
				value: "Permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 9,
				column: "ClaimType",
				value: "Permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 10,
				column: "ClaimType",
				value: "Permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 11,
				column: "ClaimType",
				value: "Permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 12,
				column: "ClaimType",
				value: "Permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 13,
				column: "ClaimType",
				value: "Permissions");

			migrationBuilder.UpdateData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 14,
				column: "ClaimType",
				value: "Permissions");

			migrationBuilder.UpdateData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "019c8b0a-4cbc-79ce-857f-1a932aff1611",
				column: "PasswordHash",
				value: "AQAAAAIAAYagAAAAEJtqiYU5rpwgqAQ+hHqcoGrhoFiHpGdYskODdMb8VTYFgiJiSh0SKSbzD2SHikaQNw==");
		}
	}
}
