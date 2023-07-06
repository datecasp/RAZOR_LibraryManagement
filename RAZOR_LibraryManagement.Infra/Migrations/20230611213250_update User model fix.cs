using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RAZOR_LibraryManagement.Infra.Migrations
{
    public partial class updateUsermodelfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumger",
                table: "Users",
                newName: "PhoneNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Users",
                newName: "PhoneNumger");
        }
    }
}
