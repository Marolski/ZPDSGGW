using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZPDSGGW.Migrations
{
    public partial class UpdateThesisTopicAndInvitationPromoterModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvitationPromoter_Promoter_PromoterId",
                table: "InvitationPromoter");

            migrationBuilder.DropForeignKey(
                name: "FK_InvitationPromoter_Student_StudentId",
                table: "InvitationPromoter");

            migrationBuilder.DropForeignKey(
                name: "FK_ThesisTopics_Promoter_PromoterId",
                table: "ThesisTopics");

            migrationBuilder.DropIndex(
                name: "IX_ThesisTopics_PromoterId",
                table: "ThesisTopics");

            migrationBuilder.DropIndex(
                name: "IX_InvitationPromoter_PromoterId",
                table: "InvitationPromoter");

            migrationBuilder.DropIndex(
                name: "IX_InvitationPromoter_StudentId",
                table: "InvitationPromoter");

            migrationBuilder.AlterColumn<Guid>(
                name: "PromoterId",
                table: "ThesisTopics",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "InvitationPromoter",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PromoterId",
                table: "InvitationPromoter",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "PromoterId",
                table: "ThesisTopics",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "InvitationPromoter",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "PromoterId",
                table: "InvitationPromoter",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_ThesisTopics_PromoterId",
                table: "ThesisTopics",
                column: "PromoterId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ThesisTopics_Promoter_PromoterId",
                table: "ThesisTopics",
                column: "PromoterId",
                principalTable: "Promoter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
