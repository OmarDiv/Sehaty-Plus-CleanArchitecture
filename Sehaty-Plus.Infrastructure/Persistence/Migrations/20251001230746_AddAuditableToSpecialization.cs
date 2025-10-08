using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sehaty_Plus.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableToSpecialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Specializations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Specializations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Specializations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Specializations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_CreatedById",
                table: "Specializations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_UpdatedById",
                table: "Specializations",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_AspNetUsers_CreatedById",
                table: "Specializations",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_AspNetUsers_UpdatedById",
                table: "Specializations",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_AspNetUsers_CreatedById",
                table: "Specializations");

            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_AspNetUsers_UpdatedById",
                table: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Specializations_CreatedById",
                table: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Specializations_UpdatedById",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Specializations");
        }
    }
}
