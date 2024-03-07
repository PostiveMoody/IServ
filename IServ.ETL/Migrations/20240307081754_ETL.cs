using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IServ.ETL.Migrations
{
    /// <inheritdoc />
    public partial class ETL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "UniversityIdSequence",
                minValue: 1L,
                maxValue: 100000L);

            migrationBuilder.CreateSequence<int>(
                name: "UniversityRawDataIdSequence",
                minValue: 1L,
                maxValue: 100000L);

            migrationBuilder.CreateTable(
                name: "University",
                columns: table => new
                {
                    UniversityId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR UniversityIdSequence"),
                    AlphaTwoCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateProvince = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebPages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domains = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UniversityVersion = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_University", x => x.UniversityId);
                });

            migrationBuilder.CreateTable(
                name: "UniversityRawData",
                columns: table => new
                {
                    UniversityRawDataId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR UniversityRawDataIdSequence"),
                    AlphaTwoCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateProvince = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebPages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Domains = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityRawData", x => x.UniversityRawDataId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "University");

            migrationBuilder.DropTable(
                name: "UniversityRawData");

            migrationBuilder.DropSequence(
                name: "UniversityIdSequence");

            migrationBuilder.DropSequence(
                name: "UniversityRawDataIdSequence");
        }
    }
}
