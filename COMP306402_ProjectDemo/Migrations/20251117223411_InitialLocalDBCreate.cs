using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace COMP306402_ProjectDemo.Migrations
{
    /// <inheritdoc />
    public partial class InitialLocalDBCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcedemicPrograms",
                columns: table => new
                {
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DurationMonths = table.Column<int>(type: "int", nullable: false),
                    TuitionFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcedemicPrograms", x => x.ProgramId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_Enrollments_AcedemicPrograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "AcedemicPrograms",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AcedemicPrograms",
                columns: new[] { "ProgramId", "Description", "DurationMonths", "Name", "TuitionFee" },
                values: new object[,]
                {
                    { 1, "Comprehensive software development curriculum.", 12, "Software Engineering", 15000.00m },
                    { 2, "Focus on network infrastructure and security.", 8, "Computer Networking", 12000.00m },
                    { 3, "Creative and technical skills for digital media.", 6, "Graphic Design Fundamentals", 8500.00m }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "EnrollmentDate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", "Doe" },
                    { 2, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane", "Smith" },
                    { 3, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ali", "Khan" },
                    { 4, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maria", "Garcia" },
                    { 5, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "David", "Lee" },
                    { 6, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emily", "Chen" },
                    { 7, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mark", "Wilson" },
                    { 8, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sarah", "Brown" },
                    { 9, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kevin", "Black" },
                    { 10, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lisa", "White" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "EnrollmentId", "EnrollmentDate", "ProgramId", "Status", "StudentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Active", 1 },
                    { 2, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Active", 2 },
                    { 3, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Active", 3 },
                    { 4, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Completed", 4 },
                    { 5, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Deferred", 5 },
                    { 6, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Active", 6 },
                    { 7, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Active", 7 },
                    { 8, new DateTime(2024, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Active", 8 },
                    { 9, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Completed", 9 },
                    { 10, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Completed", 10 },
                    { 11, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Completed", 1 },
                    { 12, new DateTime(2023, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Active", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ProgramId",
                table: "Enrollments",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "AcedemicPrograms");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
