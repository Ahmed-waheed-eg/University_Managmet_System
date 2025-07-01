using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTermRecordsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TermRecordId",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TermRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    studentId = table.Column<int>(type: "int", nullable: false),
                    SemesterId = table.Column<int>(type: "int", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    GPA = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TermRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TermRecords_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TermRecords_Students_studentId",
                        column: x => x.studentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_TermRecordId",
                table: "Enrollments",
                column: "TermRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_TermRecords_SemesterId",
                table: "TermRecords",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_TermRecords_studentId",
                table: "TermRecords",
                column: "studentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_TermRecords_TermRecordId",
                table: "Enrollments",
                column: "TermRecordId",
                principalTable: "TermRecords",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_TermRecords_TermRecordId",
                table: "Enrollments");

            migrationBuilder.DropTable(
                name: "TermRecords");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_TermRecordId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "TermRecordId",
                table: "Enrollments");
        }
    }
}
