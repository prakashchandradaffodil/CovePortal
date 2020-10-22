using Microsoft.EntityFrameworkCore.Migrations;

namespace Cove.ClassLibrary.Migrations
{
    public partial class ProfileFieldAddedInUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Profile",
                table: "UserProfile",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profile",
                table: "UserProfile");
        }
    }
}
