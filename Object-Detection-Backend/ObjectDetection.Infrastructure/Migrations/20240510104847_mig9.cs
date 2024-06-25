using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObjectDetection.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "profileurl",
                table: "aspnetusers",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash", "profileurl" },
                values: new object[] { "3732eff5-0703-42ce-bd9f-b32516eede39", "AQAAAAIAAYagAAAAEDdUkQPtPO4/lV9rfxbp5pCKIYgyDe48qK+68965aiZ2yZ+leueVqa0pi9tuRrfUUQ==", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profileurl",
                table: "aspnetusers");

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "ce901c81-3150-49a1-86cd-787da950b091", "AQAAAAIAAYagAAAAEL/5jDuWQ6SeLu5ZH2g3Q/mdQaO0p5XiimzV/XqpiPtaE8w/t12KO/nVSRa1DlOKKw==" });
        }
    }
}
