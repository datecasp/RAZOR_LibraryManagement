using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RAZOR_LibraryManagement.Infra.Migrations
{
    public partial class updateUsermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PhoneNumger");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumger",
                table: "Users",
                newName: "Password");
        }
    }
}
