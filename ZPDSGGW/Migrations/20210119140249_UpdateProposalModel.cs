using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZPDSGGW.Migrations
{
    public partial class UpdateProposalModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "Proposal");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Proposal");

            migrationBuilder.DropColumn(
                name: "PromoterName",
                table: "Proposal");

            migrationBuilder.DropColumn(
                name: "PromoterSurname",
                table: "Proposal");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Proposal");

            migrationBuilder.AddColumn<Guid>(
                name: "PromoterId",
                table: "Proposal",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "Proposal",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PromoterId",
                table: "Proposal");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Proposal");

            migrationBuilder.AddColumn<int>(
                name: "Degree",
                table: "Proposal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Proposal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PromoterName",
                table: "Proposal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PromoterSurname",
                table: "Proposal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Proposal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
