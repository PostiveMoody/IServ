using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IServ.DAL.Migrations
{
    /// <inheritdoc />
    public partial class University : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "UniversityIdSequence",
                minValue: 1L,
                maxValue: 100000L);

            migrationBuilder.CreateTable(
                name: "University",
                columns: table => new
                {
                    UniversityId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR UniversityIdSequence"),
                    AlphaTwoCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateProvince = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_University", x => x.UniversityId);
                });

            migrationBuilder.CreateTable(
                name: "WebPage",
                columns: table => new
                {
                    WebPageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebPageUrlAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebPageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPage", x => x.WebPageId);
                    table.ForeignKey(
                        name: "FK_WebPage_University_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "University",
                        principalColumn: "UniversityId");
                });

            migrationBuilder.CreateTable(
                name: "WebPageDomain",
                columns: table => new
                {
                    WebPageDomainId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebPageDomainFullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebPageDomainSecondLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebPageDomainRoot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebPageDomainProtocol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebPageDomainFourthLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebPageDomainThirdLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniversityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPageDomain", x => x.WebPageDomainId);
                    table.ForeignKey(
                        name: "FK_WebPageDomain_University_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "University",
                        principalColumn: "UniversityId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WebPage_UniversityId",
                table: "WebPage",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_WebPageDomain_UniversityId",
                table: "WebPageDomain",
                column: "UniversityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebPage");

            migrationBuilder.DropTable(
                name: "WebPageDomain");

            migrationBuilder.DropTable(
                name: "University");

            migrationBuilder.DropSequence(
                name: "UniversityIdSequence");
        }
    }
}
