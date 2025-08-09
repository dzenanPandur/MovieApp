using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieApp.Migrations
{
    /// <inheritdoc />
    public partial class userSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "BCAC500D-AFF6-47D2-BB76-6430FCC1FE83", 0, "d1f1aabf-39e5-4ae4-9a4e-7a2676e9e9cd", "admin@movieapp.com", true, false, null, "ADMIN@MOVIEAPP.COM", "ADMIN", "AQAAAAIAAYagAAAAEGQ74H878UZXZ2qrO3PGUCmbDkeR0pVC/YQ0BJQHFv50ks5DsM3WDpIZiB85F9hpRg==", null, false, "2bb56497-5c49-4c01-8bc7-e2df21ef5d53", false, "admin" },
                    { "E831C9E1-A746-4326-90FF-80BE4D20790D", 0, "f9b4e15c-1a5f-4c2e-812f-08a6c6135d5a", "user@movieapp.com", true, false, null, "USER@MOVIEAPP.COM", "USER", "AQAAAAIAAYagAAAAEGQ74H878UZXZ2qrO3PGUCmbDkeR0pVC/YQ0BJQHFv50ks5DsM3WDpIZiB85F9hpRg==", null, false, "76cbb512-37af-4f7a-aef4-4b52eb2c7618", false, "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "BCAC500D-AFF6-47D2-BB76-6430FCC1FE83");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "E831C9E1-A746-4326-90FF-80BE4D20790D");
        }
    }
}
