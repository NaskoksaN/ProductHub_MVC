using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductHub.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addProductToDbAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Category Name",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<int>(
                name: "DisplayOrder",
                table: "Categories",
                type: "int",
                nullable: false,
                comment: "Display Order",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Product name"),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false, comment: "Product Description"),
                    Price = table.Column<double>(type: "float", nullable: false, comment: "Product price"),
                    Amount = table.Column<int>(type: "int", nullable: false, comment: "Product amount"),
                    MeasurementUnit = table.Column<int>(type: "int", nullable: false, comment: "Unit of measurement")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Amount", "Description", "MeasurementUnit", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 120, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.", 0, "Soap1", 1.24 },
                    { 2, 150, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.", 0, "Soap2", 1.3999999999999999 },
                    { 3, 90, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.", 0, "Soap3", 0.89000000000000001 },
                    { 4, 140, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.", 0, "Soap4", 1.3 },
                    { 5, 70, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.", 0, "PremiumSoap1", 5.2400000000000002 },
                    { 6, 60, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.", 0, "PremiumSoap2", 4.2400000000000002 },
                    { 7, 90, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.", 0, "PremiumSoap3", 9.2400000000000002 },
                    { 8, 40, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.", 0, "PremiumSoap4", 11.24 },
                    { 9, 550, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.", 1, "ShowerGel1", 15.24 },
                    { 10, 400, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.", 1, "ShowerGel2", 21.239999999999998 },
                    { 11, 400, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc ac ullamcorper erat. Nullam quis aliquet est, sed faucibus ligula. Phasellus eu diam in sem posuere rhoncus a nec magna.", 1, "Shampoo", 112.23999999999999 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Category Name");

            migrationBuilder.AlterColumn<int>(
                name: "DisplayOrder",
                table: "Categories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Display Order");
        }
    }
}
