using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RAZOR_LibraryManagement.Infra.Migrations
{
    public partial class fourlayerseedç : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysToReturnDate",
                table: "AppSettings");

            migrationBuilder.RenameColumn(
                name: "DaysToWarningDate",
                table: "AppSettings",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "SettingParam",
                table: "AppSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "AppSettingsModelId", "SettingParam", "Value" },
                values: new object[] { 1, "DefaultFilled", 1 });

            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "AppSettingsModelId", "SettingParam", "Value" },
                values: new object[] { 2, "DaysToWarningDate", 25 });

            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "AppSettingsModelId", "SettingParam", "Value" },
                values: new object[] { 3, "DaysToReturnDate", 30 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumn: "AppSettingsModelId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumn: "AppSettingsModelId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumn: "AppSettingsModelId",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "SettingParam",
                table: "AppSettings");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "AppSettings",
                newName: "DaysToWarningDate");

            migrationBuilder.AddColumn<int>(
                name: "DaysToReturnDate",
                table: "AppSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
