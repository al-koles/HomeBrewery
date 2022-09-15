using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeBrewery.Persistence.Migrations
{
    public partial class AddedDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1, "ad4b5e0a-48fa-4a71-a796-1d53db6853ee", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 2, "142b13fc-6d23-4a36-8058-a56d887da6e8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "653f5a4f-033c-4bbc-96f9-4a1a5172e0e9", "oleksii.kolesnyk1@nure.ua", false, false, null, "OLEKSII.KOLESNYK1@NURE.UA", "OLEKSII.KOLESNYK1@NURE.UA", "AQAAAAEAACcQAAAAEHE/YBvtOT4be2GCk35aNZdRacd8XRKHFCZ20WzM45NMNIMQBfyGfY3uoJVBhXewRA==", null, false, null, false, "oleksii.kolesnyk1@nure.ua" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "HBRoleId", "HBUserId" },
                values: new object[] { 1, 1, null, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "HBRoleId", "HBUserId" },
                values: new object[] { 2, 1, null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
