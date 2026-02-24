using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Roles",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "Roles" },
                values: new object[] { "$2a$11$RJDla1K73tLW9St81vvftuKmpEXprsebCENr9m6js8T.slt/3zKNO", 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Employees");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$YKKL2Q7CluxsNJHtApBcB.ae7TnM2i7.2CPCM6nF4cmYWoz3LBbgm");
        }
    }
}
