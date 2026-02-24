using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTrackingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckOut",
                table: "Attendances",
                newName: "ClockOut");

            migrationBuilder.RenameColumn(
                name: "CheckIn",
                table: "Attendances",
                newName: "ClockIn");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClockOut",
                table: "Attendances",
                newName: "CheckOut");

            migrationBuilder.RenameColumn(
                name: "ClockIn",
                table: "Attendances",
                newName: "CheckIn");
        }
    }
}
