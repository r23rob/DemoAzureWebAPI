using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileUpload.API.Data.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    FileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    MimeType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.FileId);
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "FileId", "CreatedDate", "FileName", "MimeType" },
                values: new object[] { 1, new DateTime(2020, 3, 23, 13, 50, 6, 423, DateTimeKind.Local).AddTicks(4118), "file1.zip", "application/zip" });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "FileId", "CreatedDate", "FileName", "MimeType" },
                values: new object[] { 2, new DateTime(2020, 3, 23, 13, 50, 6, 433, DateTimeKind.Local).AddTicks(7929), "file2.zip", "application/zip" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
