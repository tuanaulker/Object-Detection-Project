using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObjectDetection.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "details",
                table: "logs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "actiontaken",
                table: "logs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "0a48f346-8ea6-494a-949b-676fb31099a1", "AQAAAAIAAYagAAAAEJ0Ce5R0jXOPmaa9b3Ny5GDGuU/eEKnZnRGN61sRRwxVAd46SHwVJ/jxlztxvQc7NA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "details",
                table: "logs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "actiontaken",
                table: "logs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "af0ae8bb-924a-47c6-a301-606bf7bd3755", "AQAAAAIAAYagAAAAEKhsZoKIOhXcVlIsEH0cRLJvZbW0EAzLIJ9HeOOKsUnpgws7u06v66ziQ/eBXRBuIA==" });
        }
    }
}
