using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductHub.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addPhoneNumberToOrderHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StreetAddress",
                table: "OrderHeaders",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "",
                comment: "User street address",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true,
                oldComment: "User street address");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "OrderHeaders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                comment: "User postal code",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldComment: "User postal code");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "OrderHeaders",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "",
                comment: "User city",
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldNullable: true,
                oldComment: "User city");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "Phone Number");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "OrderHeaders");

            migrationBuilder.AlterColumn<string>(
                name: "StreetAddress",
                table: "OrderHeaders",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                comment: "User street address",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldComment: "User street address");

            migrationBuilder.AlterColumn<string>(
                name: "PostalCode",
                table: "OrderHeaders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                comment: "User postal code",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldComment: "User postal code");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "OrderHeaders",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: true,
                comment: "User city",
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70,
                oldComment: "User city");
        }
    }
}
