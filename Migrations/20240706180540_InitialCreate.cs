using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BiteWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "Description" },
                values: new object[,]
                {
                    { 1, "Fruit Pies", "Pies made with various fruits" },
                    { 2, "Cakes", "Various types of cakes" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "AllergyInformation", "CategoryId", "ImageThumbnailUrl", "ImageUrl", "InStock", "IsPieOfTheWeek", "LongDescription", "Name", "Price", "ShortDescription" },
                values: new object[,]
                {
                    { 1, "Contains gluten", 1, "https://example.com/apple-pie-thumb.jpg", "https://example.com/apple-pie.jpg", true, true, "A classic dessert made with fresh apples and cinnamon, baked to perfection.", "Apple Item", 12.99m, "Delicious apple pie" },
                    { 2, "Contains dairy, gluten", 2, "https://example.com/chocolate-cake-thumb.jpg", "https://example.com/chocolate-cake.jpg", true, false, "Decadent chocolate cake layered with chocolate ganache and topped with chocolate shavings.", "Chocolate Cake", 19.99m, "Rich chocolate cake" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);
        }
    }
}
