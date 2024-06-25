using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObjectDetection.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "areas",
                table: "logs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "ce901c81-3150-49a1-86cd-787da950b091", "AQAAAAIAAYagAAAAEL/5jDuWQ6SeLu5ZH2g3Q/mdQaO0p5XiimzV/XqpiPtaE8w/t12KO/nVSRa1DlOKKw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "areas",
                table: "logs");

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "21dda804-99f4-4269-aa6f-d916831121cf", "AQAAAAIAAYagAAAAEB9xkJxgkck085yiPWFTHaO+STVU8HI3OBIxFf8MNhyiMvAKkXdALte4xOzMICeTdA==" });
        }
    }
}
