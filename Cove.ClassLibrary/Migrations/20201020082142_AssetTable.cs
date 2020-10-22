using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cove.ClassLibrary.Migrations
{
    public partial class AssetTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePaths",
                table: "UserProfile");

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetId = table.Column<Guid>(nullable: false),
                    AssetValue = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetId);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileAssets",
                columns: table => new
                {
                    UserProfileAssetId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    AssetId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileAssets", x => x.UserProfileAssetId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "UserProfileAssets");

            migrationBuilder.AddColumn<string>(
                name: "FilePaths",
                table: "UserProfile",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
