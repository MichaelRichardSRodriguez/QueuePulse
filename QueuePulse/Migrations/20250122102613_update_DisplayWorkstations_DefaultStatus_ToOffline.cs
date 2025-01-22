using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueuePulse.Migrations
{
    /// <inheritdoc />
    public partial class update_DisplayWorkstations_DefaultStatus_ToOffline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "DisplayWorkstations",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "Offline",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldDefaultValue: "Active");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "DisplayWorkstations",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "Active",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldDefaultValue: "Offline");
        }
    }
}
