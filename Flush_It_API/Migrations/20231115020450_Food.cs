using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Flush_It_API.Migrations
{
    /// <inheritdoc />
    public partial class Food : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Calories = table.Column<int>(type: "integer", nullable: true),
                    Sugar = table.Column<double>(type: "double precision", nullable: true),
                    Sodium = table.Column<double>(type: "double precision", nullable: true),
                    Potassium = table.Column<double>(type: "double precision", nullable: true),
                    Calcium = table.Column<double>(type: "double precision", nullable: true),
                    Iron = table.Column<double>(type: "double precision", nullable: true),
                    Carbs = table.Column<double>(type: "double precision", nullable: true),
                    Protein = table.Column<double>(type: "double precision", nullable: true),
                    TotalFat = table.Column<double>(type: "double precision", nullable: true),
                    SaturatedFat = table.Column<double>(type: "double precision", nullable: true),
                    TransFat = table.Column<double>(type: "double precision", nullable: true),
                    Cholesterol = table.Column<double>(type: "double precision", nullable: true),
                    Fiber = table.Column<double>(type: "double precision", nullable: true),
                    Alcohol = table.Column<bool>(type: "boolean", nullable: true),
                    Processed = table.Column<bool>(type: "boolean", nullable: true),
                    FODMAP = table.Column<bool>(type: "boolean", nullable: true),
                    DairyProduct = table.Column<bool>(type: "boolean", nullable: true),
                    Gluten = table.Column<bool>(type: "boolean", nullable: true),
                    Spicy = table.Column<bool>(type: "boolean", nullable: true),
                    Fried = table.Column<bool>(type: "boolean", nullable: true),
                    Caffeine = table.Column<bool>(type: "boolean", nullable: true),
                    Sweetener = table.Column<bool>(type: "boolean", nullable: true),
                    Bread = table.Column<bool>(type: "boolean", nullable: true),
                    ArtificialAdditives = table.Column<bool>(type: "boolean", nullable: true),
                    CarbonatedBeverage = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Food");
        }
    }
}
