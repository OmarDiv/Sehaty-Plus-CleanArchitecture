using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sehaty_Plus.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Step 1: Drop foreign key constraint from DoctorClinics that references Doctors.Id
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorClinics_Doctors_DoctorId",
                table: "DoctorClinics");

            // Step 2: Drop primary key constraints
            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            // Step 3: Alter column types (removed oldDefaultValueSql because string doesn't need it)
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "DoctorClinics",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            // Step 4: Recreate primary key constraints
            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "Id");

            // Step 5: Recreate foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_DoctorClinics_Doctors_DoctorId",
                table: "DoctorClinics",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Reverse order: Drop foreign key first
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorClinics_Doctors_DoctorId",
                table: "DoctorClinics");

            // Drop primary keys
            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            // Alter columns back to Guid with default constraint
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "DoctorId",
                table: "DoctorClinics",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            // Recreate primary keys
            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "Id");

            // Recreate foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_DoctorClinics_Doctors_DoctorId",
                table: "DoctorClinics",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}