using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom.Inferastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "ID", "ImageName", "ProductId" },
                values: new object[] { 3, "test", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
