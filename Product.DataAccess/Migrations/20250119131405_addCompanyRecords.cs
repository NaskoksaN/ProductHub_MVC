using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductHub.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addCompanyRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "PostalCode", "StreetAddress", "VAT" },
                values: new object[,]
                {
                    { 1, "Varna", "Nasko Bobchev Ltd", "0012312334", "BG 9000", "jk Pobeda, str Todor radev Penev 7 - ap16", "BG41023450" },
                    { 2, "Haskovo", "Nasko  Ltd", "0023312334", "BG 6300", "str Gurgulqt 2, ent.B , app58", "BG41023123" },
                    { 3, "selo Kukovica", "Mega company Ltd", "01223312334", "BG 1300", "str GPetko petkov 12", "BG41034123" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
