using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Survey_Basket.Persistance.Migrations
{
	/// <inheritdoc />
	public partial class SeedIdentityTables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
				values: new object[,]
				{
					{ "019c8b1c-a013-7887-b113-fb4f89d0892b", "019c8b22-5b59-7f75-84c0-4c519114bf58", false, false, "Admin", "ADMIN" },
					{ "019c8b21-6b91-7716-ba12-cb5f6b8d9655", "019c8b22-9cd7-7f61-a1fe-6eaeeb15ff6f", true, false, "Member", "MEMBER" }
				});

			migrationBuilder.InsertData(
				table: "AspNetUsers",
				columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
				values: new object[] { "019c8b0a-4cbc-79ce-857f-1a932aff1611", 0, "019c8b0d-607c-7a73-b5a1-c5e669c38dc3", "Admin@Survey-Basket.com", true, "Survey-Basket", "Admin", false, null, "ADMIN@SURVEY-BASKET.COM", "ADMIN@SURVEY-BASKET.COM", "AQAAAAIAAYagAAAAEJtqiYU5rpwgqAQ+hHqcoGrhoFiHpGdYskODdMb8VTYFgiJiSh0SKSbzD2SHikaQNw==", null, false, "019C8B0C4F5E7A678CFD689AEE63EA5F", false, "Admin@Survey-Basket.com" });

			migrationBuilder.InsertData(
				table: "AspNetRoleClaims",
				columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
				values: new object[,]
				{
					{ 1, "Permissions", "Polls:Read", "019c8b1c-a013-7887-b113-fb4f89d0892b" },
					{ 2, "Permissions", "Polls:Add", "019c8b1c-a013-7887-b113-fb4f89d0892b" },
					{ 3, "Permissions", "Polls:Update", "019c8b1c-a013-7887-b113-fb4f89d0892b" },
					{ 4, "Permissions", "Polls:Delete", "019c8b1c-a013-7887-b113-fb4f89d0892b" },
					{ 5, "Permissions", "Questions:Read", "019c8b1c-a013-7887-b113-fb4f89d0892b" },
					{ 6, "Permissions", "Questions:Add", "019c8b1c-a013-7887-b113-fb4f89d0892b" },
					{ 7, "Permissions", "Questions:Update", "019c8b1c-a013-7887-b113-fb4f89d0892b" },
					{ 8, "Permissions", "Accounts:Read", "019c8b1c-a013-7887-b113-fb4f89d0892b" },
					{ 9, "Permissions", "Accounts:Add", "019c8b1c-a013-7887-b113-fb4f89d0892b" },
					{ 10, "Permissions", "Accounts:Update", "019c8b1c-a013-7887-b113-fb4f89d0892b" },
					{ 11, "Permissions", "Roles:Read", "019c8b1c-a013-7887-b113-fb4f89d0892b" },
					{ 12, "Permissions", "Roles:Add", "019c8b1c-a013-7887-b113-fb4f89d0892b" },
					{ 13, "Permissions", "Roles:Update", "019c8b1c-a013-7887-b113-fb4f89d0892b" },
					{ 14, "Permissions", "Results:Read", "019c8b1c-a013-7887-b113-fb4f89d0892b" }
				});

			migrationBuilder.InsertData(
				table: "AspNetUserRoles",
				columns: new[] { "RoleId", "UserId" },
				values: new object[] { "019c8b1c-a013-7887-b113-fb4f89d0892b", "019c8b0a-4cbc-79ce-857f-1a932aff1611" });
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 1);

			migrationBuilder.DeleteData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 2);

			migrationBuilder.DeleteData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 3);

			migrationBuilder.DeleteData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 4);

			migrationBuilder.DeleteData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 5);

			migrationBuilder.DeleteData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 6);

			migrationBuilder.DeleteData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 7);

			migrationBuilder.DeleteData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 8);

			migrationBuilder.DeleteData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 9);

			migrationBuilder.DeleteData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 10);

			migrationBuilder.DeleteData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 11);

			migrationBuilder.DeleteData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 12);

			migrationBuilder.DeleteData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 13);

			migrationBuilder.DeleteData(
				table: "AspNetRoleClaims",
				keyColumn: "Id",
				keyValue: 14);

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "019c8b21-6b91-7716-ba12-cb5f6b8d9655");

			migrationBuilder.DeleteData(
				table: "AspNetUserRoles",
				keyColumns: new[] { "RoleId", "UserId" },
				keyValues: new object[] { "019c8b1c-a013-7887-b113-fb4f89d0892b", "019c8b0a-4cbc-79ce-857f-1a932aff1611" });

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "019c8b1c-a013-7887-b113-fb4f89d0892b");

			migrationBuilder.DeleteData(
				table: "AspNetUsers",
				keyColumn: "Id",
				keyValue: "019c8b0a-4cbc-79ce-857f-1a932aff1611");
		}
	}
}
