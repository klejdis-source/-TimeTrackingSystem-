using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$YKKL2Q7CluxsNJHtApBcB.ae7TnM2i7.2CPCM6nF4cmYWoz3LBbgm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$ev7LzH.6Yf3DovD29.7G9uRPhYpUfH6r7G.RjXGqY5U1K/6Xh/vG.");
        }
    }
}
