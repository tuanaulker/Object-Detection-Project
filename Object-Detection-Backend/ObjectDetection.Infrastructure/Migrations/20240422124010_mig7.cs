using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ObjectDetection.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "publishedlogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    logid = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    isshow = table.Column<bool>(type: "boolean", nullable: false),
                    createdtime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    showntime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publishedlogs", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "21dda804-99f4-4269-aa6f-d916831121cf", "AQAAAAIAAYagAAAAEB9xkJxgkck085yiPWFTHaO+STVU8HI3OBIxFf8MNhyiMvAKkXdALte4xOzMICeTdA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "publishedlogs");

            migrationBuilder.UpdateData(
                table: "aspnetusers",
                keyColumn: "id",
                keyValue: "94c328af-952d-42a5-ae86-4f0fe6d84d74",
                columns: new[] { "concurrencystamp", "passwordhash" },
                values: new object[] { "4d0fbbe9-dfae-4bfb-b2f2-8ec36ca19ea4", "AQAAAAIAAYagAAAAEOjI4m2oNPGMTlijrMtiuxzlJs6TWca9BqTEznxlQf/RGxILD7UZLUDht5ane5wyEQ==" });
        }
    }
}
