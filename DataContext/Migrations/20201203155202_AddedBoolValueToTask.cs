using Microsoft.EntityFrameworkCore.Migrations;

namespace DataContext.Migrations
{
    public partial class AddedBoolValueToTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isCompleted",
                table: "Tasks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCompleted",
                table: "Tasks");
        }
    }
}
