using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZPDSGGW.Migrations
{
    public partial class AddPromoterToThesisModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NamePromoter",
                table: "ThesisTopics");

            migrationBuilder.DropColumn(
                name: "SurnamePromoter",
                table: "ThesisTopics");

            migrationBuilder.AddColumn<Guid>(
                name: "PromoterId",
                table: "ThesisTopics",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThesisTopics_PromoterId",
                table: "ThesisTopics",
                column: "PromoterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThesisTopics_Promoter_PromoterId",
                table: "ThesisTopics",
                column: "PromoterId",
                principalTable: "Promoter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThesisTopics_Promoter_PromoterId",
                table: "ThesisTopics");

            migrationBuilder.DropIndex(
                name: "IX_ThesisTopics_PromoterId",
                table: "ThesisTopics");

            migrationBuilder.DropColumn(
                name: "PromoterId",
                table: "ThesisTopics");

            migrationBuilder.AddColumn<string>(
                name: "NamePromoter",
                table: "ThesisTopics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SurnamePromoter",
                table: "ThesisTopics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
