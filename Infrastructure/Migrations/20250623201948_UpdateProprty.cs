using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProprty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "SuperAdmins",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Admins",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SuperAdmins",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Admins",
                newName: "FullName");
        }
    }
}
