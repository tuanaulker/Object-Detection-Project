using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObjectDetection.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "actiontime",
                table: "logs",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "confidence",
                table: "logs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "safezone",
                table: "logs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "7a8be177-bfb9-43b5-ae58-2d847d19c6b8", "AQAAAAIAAYagAAAAEBAQnso+R8WdZE39ttQWdrZxmwHS0PYN3bBesOs6VV3G/G3ZzoYoiKsa4iyRnzdtGg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "confidence",
                table: "logs");

            migrationBuilder.DropColumn(
                name: "safezone",
                table: "logs");

            migrationBuilder.AlterColumn<DateTime>(
                name: "actiontime",
                table: "logs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "0a48f346-8ea6-494a-949b-676fb31099a1", "AQAAAAIAAYagAAAAEJ0Ce5R0jXOPmaa9b3Ny5GDGuU/eEKnZnRGN61sRRwxVAd46SHwVJ/jxlztxvQc7NA==" });
        }
    }
}
