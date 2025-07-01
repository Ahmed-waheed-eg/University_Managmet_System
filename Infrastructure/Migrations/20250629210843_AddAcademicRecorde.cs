using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAcademicRecorde : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_OfferedCourses_OfferedCourseId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Enrollments",
                newName: "AcademicRecordId");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Enrollments",
                newName: "IsPassed");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                newName: "IX_Enrollments_AcademicRecordId");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AcademicRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    semesterId = table.Column<int>(type: "int", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicRecords_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AcademicRecords_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AcademicRecords_Semesters_semesterId",
                        column: x => x.semesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AcademicRecords_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRecords_DepartmentId",
                table: "AcademicRecords",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRecords_LevelId",
                table: "AcademicRecords",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRecords_semesterId",
                table: "AcademicRecords",
                column: "semesterId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicRecords_StudentId",
                table: "AcademicRecords",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_AcademicRecords_AcademicRecordId",
                table: "Enrollments",
                column: "AcademicRecordId",
                principalTable: "AcademicRecords",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_OfferedCourses_OfferedCourseId",
                table: "Enrollments",
                column: "OfferedCourseId",
                principalTable: "OfferedCourses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_AcademicRecords_AcademicRecordId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_OfferedCourses_OfferedCourseId",
                table: "Enrollments");

            migrationBuilder.DropTable(
                name: "AcademicRecords");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "IsPassed",
                table: "Enrollments",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "AcademicRecordId",
                table: "Enrollments",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_AcademicRecordId",
                table: "Enrollments",
                newName: "IX_Enrollments_StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_OfferedCourses_OfferedCourseId",
                table: "Enrollments",
                column: "OfferedCourseId",
                principalTable: "OfferedCourses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
