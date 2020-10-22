using Microsoft.EntityFrameworkCore.Migrations;

namespace Cove.ClassLibrary.Migrations
{
    public partial class uploadcomicmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadComic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UploadComicAssetId = table.Column<string>(nullable: false),
                    Creator = table.Column<string>(nullable: false),
                    SeriesTitle = table.Column<string>(nullable: true),
                    IssueTitle = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    IssueNumber = table.Column<string>(maxLength: 4, nullable: false),
                    SelectedAgeSuitabilityId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    UploadComicThumbnailAssetId = table.Column<string>(nullable: true),
                    isPublishMyComic = table.Column<bool>(nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadComic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadComicAgeAvailabilityMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadComicAgeAvailabilityMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadComicContentTypeMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadComicContentTypeMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadComicFictionMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadComicFictionMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadComicNonFictionMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadComicNonFictionMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadComicTagTypeMaster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadComicTagTypeMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadedComicContentType",
                columns: table => new
                {
                    ComicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedComicContentType", x => x.ComicId);
                });

            migrationBuilder.CreateTable(
                name: "UploadedComicFiction",
                columns: table => new
                {
                    ComicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FictionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedComicFiction", x => x.ComicId);
                });

            migrationBuilder.CreateTable(
                name: "UploadedComicNonFiction",
                columns: table => new
                {
                    ComicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NonFictionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedComicNonFiction", x => x.ComicId);
                });

            migrationBuilder.CreateTable(
                name: "UploadedComicTagType",
                columns: table => new
                {
                    ComicId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedComicTagType", x => x.ComicId);
                });

            migrationBuilder.InsertData(
                table: "UploadComicAgeAvailabilityMaster",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 1, "All Ages" },
                    { 2, "Teen and Up" },
                    { 3, "Adult" }
                });

            migrationBuilder.InsertData(
                table: "UploadComicContentTypeMaster",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 11, "Violence" },
                    { 9, "Sexism" },
                    { 8, "Self Harm" },
                    { 7, "Rape / Non - consent" },
                    { 6, "Racism" },
                    { 10, "Swearing" },
                    { 4, "Gore" },
                    { 3, "Explicit" },
                    { 2, "Character Death" },
                    { 1, "Blasphemy" },
                    { 5, "Out Dated Ideals" }
                });

            migrationBuilder.InsertData(
                table: "UploadComicFictionMaster",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 15, "Romance" },
                    { 16, "Satire" },
                    { 17, "Science Fiction" },
                    { 19, "Superhero" },
                    { 20, "Suspense" },
                    { 22, "Western" },
                    { 23, "Women’s Fiction" },
                    { 24, "War" },
                    { 25, "Horror" },
                    { 14, "Political" },
                    { 21, "Thriller" },
                    { 13, "Paranormalac" },
                    { 18, "Spy" },
                    { 11, "LGBTQ" },
                    { 12, "Mystery" },
                    { 1, "Action / Adventure" },
                    { 3, "Classics" },
                    { 4, "Crime" },
                    { 5, "Detective" },
                    { 2, "Chick Lit" },
                    { 7, "Fairytale" },
                    { 8, "Fantasy" },
                    { 9, "Historical Fiction" },
                    { 10, "Humour" },
                    { 6, "Drama" }
                });

            migrationBuilder.InsertData(
                table: "UploadComicNonFictionMaster",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 18, "War" },
                    { 11, "Religion / Spirituality" },
                    { 17, "True Crime" },
                    { 16, "Travel" },
                    { 15, "Sports & Leisure" },
                    { 14, "Self Help" },
                    { 13, "Science" },
                    { 12, "Review" },
                    { 10, "Poetry" },
                    { 7, "LGBTQ" },
                    { 8, "Memoir" },
                    { 6, "Home & Garden" },
                    { 5, "History" },
                    { 4, "Health / Fitness" },
                    { 3, "Biography" },
                    { 2, "Autobiography" },
                    { 1, "Art" },
                    { 9, "Philosophy" }
                });

            migrationBuilder.InsertData(
                table: "UploadComicTagTypeMaster",
                columns: new[] { "Id", "Value" },
                values: new object[,]
                {
                    { 6, "Collection" },
                    { 1, "Stand alone" },
                    { 2, "Mini Series" },
                    { 3, "Ongoing Story" },
                    { 4, "Anthology" },
                    { 5, "Graphic Novel" },
                    { 7, "Manga" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadComic");

            migrationBuilder.DropTable(
                name: "UploadComicAgeAvailabilityMaster");

            migrationBuilder.DropTable(
                name: "UploadComicContentTypeMaster");

            migrationBuilder.DropTable(
                name: "UploadComicFictionMaster");

            migrationBuilder.DropTable(
                name: "UploadComicNonFictionMaster");

            migrationBuilder.DropTable(
                name: "UploadComicTagTypeMaster");

            migrationBuilder.DropTable(
                name: "UploadedComicContentType");

            migrationBuilder.DropTable(
                name: "UploadedComicFiction");

            migrationBuilder.DropTable(
                name: "UploadedComicNonFiction");

            migrationBuilder.DropTable(
                name: "UploadedComicTagType");
        }
    }
}
