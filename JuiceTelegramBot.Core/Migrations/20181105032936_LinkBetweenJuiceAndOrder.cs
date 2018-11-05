using Microsoft.EntityFrameworkCore.Migrations;

namespace JuiceTelegramBot.Core.Migrations
{
    public partial class LinkBetweenJuiceAndOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "JuiceId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCustom",
                table: "Juices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_JuiceId",
                table: "Orders",
                column: "JuiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Juices_JuiceId",
                table: "Orders",
                column: "JuiceId",
                principalTable: "Juices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Juices_JuiceId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_JuiceId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "JuiceId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsCustom",
                table: "Juices");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Orders",
                nullable: false,
                defaultValue: "");
        }
    }
}
