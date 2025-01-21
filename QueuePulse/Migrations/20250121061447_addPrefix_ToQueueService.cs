using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueuePulse.Migrations
{
    /// <inheritdoc />
    public partial class addPrefix_ToQueueService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                table: "QueueServices",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prefix",
                table: "QueueServices");
        }
    }
}
