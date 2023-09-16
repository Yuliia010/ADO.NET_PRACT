
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pract_15092023.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCards_Students_Id",
                        column: x => x.Id,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "LastName", "MailAddress", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2005, 9, 16, 16, 21, 0, 918, DateTimeKind.Local).AddTicks(8549), "Ivanov", "ii@gmail.com", "Ivan", "380665544488" },
                    { 2, new DateTime(2003, 9, 16, 16, 21, 0, 918, DateTimeKind.Local).AddTicks(8554), "Ivanov", "ii@gmail.com", "Ivan", "380665544488" },
                    { 3, new DateTime(1999, 9, 16, 16, 21, 0, 918, DateTimeKind.Local).AddTicks(8557), "Ivanov", "ii@gmail.com", "Ivan", "380665544488" }
                });

            migrationBuilder.InsertData(
                table: "StudentCards",
                columns: new[] { "Id", "CardNumber", "ExpireDate", "IsActive" },
                values: new object[,]
                {
                    { 1, "HG-145899", new DateTime(2024, 12, 16, 16, 21, 0, 918, DateTimeKind.Local).AddTicks(8415), true },
                    { 2, "AG-145111", new DateTime(2024, 9, 16, 16, 21, 0, 918, DateTimeKind.Local).AddTicks(8448), true },
                    { 3, "AG-145111", new DateTime(2023, 7, 16, 16, 21, 0, 918, DateTimeKind.Local).AddTicks(8450), false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCards");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
