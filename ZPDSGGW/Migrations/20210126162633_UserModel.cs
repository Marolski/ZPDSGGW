using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZPDSGGW.Migrations
{
    public partial class UserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Promoter");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Degrees = table.Column<int>(type: "int", nullable: false),
                    Availability = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.CreateTable(
                name: "Promoter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Availability = table.Column<int>(type: "int", nullable: false),
                    Degrees = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promoter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Promoter",
                columns: new[] { "Id", "Availability", "Degrees", "Name", "Surname" },
                values: new object[] { new Guid("f2f6784c-cac2-4949-b81d-63584314d797"), 0, 3, "Promotor1", "Test" });

            migrationBuilder.InsertData(
                table: "Promoter",
                columns: new[] { "Id", "Availability", "Degrees", "Name", "Surname" },
                values: new object[] { new Guid("3b00f1d2-f701-4b63-9a22-5c3d602bf7d2"), 0, 7, "Promotor2", "Test" });

            migrationBuilder.InsertData(
                table: "Promoter",
                columns: new[] { "Id", "Availability", "Degrees", "Name", "Surname" },
                values: new object[] { new Guid("5a1298d3-c95b-4f0e-96d9-1b1881653777"), 0, 4, "Promotor3", "Test" });
        }
    }
}
