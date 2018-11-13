using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JuiceTelegramBot.Core.Migrations
{
    public partial class AddNewFieldsintoJuiceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Juices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "JuiceDateTime",
                table: "Juices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Juices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Juices");

            migrationBuilder.DropColumn(
                name: "JuiceDateTime",
                table: "Juices");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Juices");
        }
    }
}
