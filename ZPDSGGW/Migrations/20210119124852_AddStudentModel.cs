using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZPDSGGW.Migrations
{
    public partial class AddStudentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "InvitationPromoter");

            migrationBuilder.DropColumn(
                name: "PromoterName",
                table: "InvitationPromoter");

            migrationBuilder.DropColumn(
                name: "PromoterSurname",
                table: "InvitationPromoter");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "InvitationPromoter");

            migrationBuilder.DropColumn(
                name: "degrees",
                table: "InvitationPromoter");

            migrationBuilder.AddColumn<Guid>(
                name: "PromoterId",
                table: "InvitationPromoter",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "InvitationPromoter",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvitationPromoter_PromoterId",
                table: "InvitationPromoter",
                column: "PromoterId");

            migrationBuilder.CreateIndex(
                name: "IX_InvitationPromoter_StudentId",
                table: "InvitationPromoter",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvitationPromoter_Promoter_PromoterId",
                table: "InvitationPromoter",
                column: "PromoterId",
                principalTable: "Promoter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvitationPromoter_Student_StudentId",
                table: "InvitationPromoter",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvitationPromoter_Promoter_PromoterId",
                table: "InvitationPromoter");

            migrationBuilder.DropForeignKey(
                name: "FK_InvitationPromoter_Student_StudentId",
                table: "InvitationPromoter");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropIndex(
                name: "IX_InvitationPromoter_PromoterId",
                table: "InvitationPromoter");

            migrationBuilder.DropIndex(
                name: "IX_InvitationPromoter_StudentId",
                table: "InvitationPromoter");

            migrationBuilder.DropColumn(
                name: "PromoterId",
                table: "InvitationPromoter");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "InvitationPromoter");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "InvitationPromoter",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PromoterName",
                table: "InvitationPromoter",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PromoterSurname",
                table: "InvitationPromoter",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "InvitationPromoter",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "degrees",
                table: "InvitationPromoter",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
