using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sehaty_Plus.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSpecializationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Doctors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
