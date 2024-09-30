using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoubleVPartners.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "tblRole",
                schema: "dbo",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRole = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRole", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                schema: "dbo",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRole = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_tblUser_tblRole_IdRole",
                        column: x => x.IdRole,
                        principalSchema: "dbo",
                        principalTable: "tblRole",
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblTask",
                schema: "dbo",
                columns: table => new
                {
                    IdTask = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<int>(type: "int", nullable: true),
                    NameTask = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StatusTask = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTask", x => x.IdTask);
                    table.ForeignKey(
                        name: "FK_tblTask_tblUser_IdUser",
                        column: x => x.IdUser,
                        principalSchema: "dbo",
                        principalTable: "tblUser",
                        principalColumn: "IdUser");
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tblRole",
                columns: new[] { "IdRole", "NameRole" },
                values: new object[] { 1, "Administrador" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tblRole",
                columns: new[] { "IdRole", "NameRole" },
                values: new object[] { 2, "Supervisor" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tblRole",
                columns: new[] { "IdRole", "NameRole" },
                values: new object[] { 3, "Empleado" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "tblUser",
                columns: new[] { "IdUser", "Email", "IdRole", "IsActive", "Name", "Password", "PhoneNumber" },
                values: new object[] { 1, "admin@admin.com", 1, true, "Admin", "PjJ2Yv6RFgk=", "1234567890" });

            migrationBuilder.CreateIndex(
                name: "IX_tblTask_IdUser",
                schema: "dbo",
                table: "tblTask",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_IdRole",
                schema: "dbo",
                table: "tblUser",
                column: "IdRole");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblTask",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tblUser",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tblRole",
                schema: "dbo");
        }
    }
}
