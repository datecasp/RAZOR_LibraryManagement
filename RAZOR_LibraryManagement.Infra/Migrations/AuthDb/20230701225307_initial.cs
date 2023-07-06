using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RAZOR_LibraryManagement.Infra.Migrations.AuthDb
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aDMiN",
                column: "NormalizedName",
                value: "ADMIN");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "SuPeRaDMiN",
                column: "NormalizedName",
                value: "SUPERADMIN");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "SuPeRaDMiNiD",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c9314ec-2786-4622-b084-b4b234b43b5c", "superAdmin@mail.com", "SUPERADMIN@MAIL.COM", "SUPERADMIN", "AQAAAAEAACcQAAAAEHaTd2zTi88UNKdLuex27G8w/Msc4F/94/ZPKAxO+VwwsF1pr8JHI1GGVxR3r3+0/Q==", "4dd606bf-f1a3-4bdc-8d33-e6fb02aaa7c7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aDMiN",
                column: "NormalizedName",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "SuPeRaDMiN",
                column: "NormalizedName",
                value: "SuperAdmin");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "SuPeRaDMiNiD",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b457db8-3f7d-4a81-a798-55fa087f38a4", "superAdminId@mail.com", null, null, "AQAAAAEAACcQAAAAEBJPrNvVaUkQrbQUhFqO9xE5K5GV8TcxKnm2ugUSnis9gzqYqgMBxpWSj6VVdWnFxQ==", "d4453d70-3ca4-4f83-ad2c-95cebb25aa67" });
        }
    }
}
