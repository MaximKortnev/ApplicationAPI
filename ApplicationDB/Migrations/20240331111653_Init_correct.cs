using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationDB.Migrations
{
    /// <inheritdoc />
    public partial class Init_correct : Migration
    {
        private readonly string _oldColumnName = "Plan";
        private readonly string _newColumnName = "Outline";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: _oldColumnName,
                table: "Applications",
                newName: _newColumnName);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: _newColumnName,
                table: "Applications",
                newName: _oldColumnName);
        }
    }
}