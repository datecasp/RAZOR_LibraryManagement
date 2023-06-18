using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RAZOR_LibraryManagement.Infra.Migrations
{
    public partial class key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppSettingsModelId",
                table: "AppSettings",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AppSettings",
                newName: "AppSettingsModelId");
        }
    }
}
