using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.WebApi.Migrations
{
    public partial class AddSampleDataFromDbTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SampleDataFromDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StringData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuidData = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoubleData = table.Column<double>(type: "float", nullable: false),
                    DecimalData = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EnumData = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleDataFromDb", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SampleDataFromDb");
        }
    }
}
