﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductHub.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addSessionIdToOrderHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SeesionId",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeesionId",
                table: "OrderHeaders");
        }
    }
}
