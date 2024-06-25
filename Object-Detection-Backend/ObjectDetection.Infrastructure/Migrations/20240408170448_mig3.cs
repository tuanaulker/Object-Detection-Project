using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ObjectDetection.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_logs",
                table: "logs");

            migrationBuilder.DropColumn(
                name: "logid",
                table: "logs");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "logs",
                newName: "capturedtime");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "logs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "pk_logs",
                table: "logs",
                column: "id");

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "af0ae8bb-924a-47c6-a301-606bf7bd3755", "AQAAAAIAAYagAAAAEKhsZoKIOhXcVlIsEH0cRLJvZbW0EAzLIJ9HeOOKsUnpgws7u06v66ziQ/eBXRBuIA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_logs",
                table: "logs");

            migrationBuilder.DropColumn(
                name: "id",
                table: "logs");

            migrationBuilder.RenameColumn(
                name: "capturedtime",
                table: "logs",
                newName: "time");

            migrationBuilder.AddColumn<int>(
                name: "logid",
                table: "logs",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_logs",
                table: "logs",
                column: "logid");

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "aa7b8118-e554-41a1-bca8-9eeba765b126", "AQAAAAIAAYagAAAAEJq3TQfMrUXIydPTMoeBz3ofLyh4oHWO/kdlL7djHek4ON5sKQdRA2i5in31aMdJRQ==" });
        }
    }
}
