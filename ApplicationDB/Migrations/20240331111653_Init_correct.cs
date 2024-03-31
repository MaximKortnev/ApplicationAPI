using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationDB.Migrations
{
    /// <inheritdoc />
    public partial class Init_correct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Plan",
                table: "Applications",
                newName: "Outline");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Outline",
                table: "Applications",
                newName: "Plan");
        }
    }
}
