using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSD_Lab2.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCost = table.Column<double>(type: "float", nullable: false),
                    ProductColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductMaterial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginLocation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
