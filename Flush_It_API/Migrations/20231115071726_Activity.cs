using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Flush_It_API.Migrations
{
    /// <inheritdoc />
    public partial class Activity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityId",
                table: "Food",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    HadAttack = table.Column<bool>(type: "boolean", nullable: false),
                    IsHealthyDay = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodActivities",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "integer", nullable: false),
                    ActivityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodActivities", x => new { x.FoodId, x.ActivityId });
                    table.ForeignKey(
                        name: "FK_FoodActivities_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodActivities_Food_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_ActivityId",
                table: "Food",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodActivities_ActivityId",
                table: "FoodActivities",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Activities_ActivityId",
                table: "Food",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Activities_ActivityId",
                table: "Food");

            migrationBuilder.DropTable(
                name: "FoodActivities");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Food_ActivityId",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Food");
        }
    }
}
