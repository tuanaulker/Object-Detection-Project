using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObjectDetection.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "userid",
                table: "logs",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<bool>(
                name: "ispublished",
                table: "logs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "4d0fbbe9-dfae-4bfb-b2f2-8ec36ca19ea4", "AQAAAAIAAYagAAAAEOjI4m2oNPGMTlijrMtiuxzlJs6TWca9BqTEznxlQf/RGxILD7UZLUDht5ane5wyEQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ispublished",
                table: "logs");

            migrationBuilder.AlterColumn<Guid>(
                name: "userid",
                table: "logs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "7a8be177-bfb9-43b5-ae58-2d847d19c6b8", "AQAAAAIAAYagAAAAEBAQnso+R8WdZE39ttQWdrZxmwHS0PYN3bBesOs6VV3G/G3ZzoYoiKsa4iyRnzdtGg==" });
        }
    }
}
