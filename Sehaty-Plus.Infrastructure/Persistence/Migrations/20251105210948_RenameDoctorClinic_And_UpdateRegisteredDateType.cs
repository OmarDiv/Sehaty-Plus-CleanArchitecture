using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sehaty_Plus.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameDoctorClinic_And_UpdateRegisteredDateType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorClinic_Clinics_ClinicId",
                table: "DoctorClinic");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorClinic_Doctors_DoctorId",
                table: "DoctorClinic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorClinic",
                table: "DoctorClinic");

            migrationBuilder.RenameTable(
                name: "DoctorClinic",
                newName: "DoctorClinics");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorClinic_DoctorId",
                table: "DoctorClinics",
                newName: "IX_DoctorClinics_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorClinic_ClinicId",
                table: "DoctorClinics",
                newName: "IX_DoctorClinics_ClinicId");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "RegisteredDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorClinics",
                table: "DoctorClinics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorClinics_Clinics_ClinicId",
                table: "DoctorClinics",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorClinics_Clinics_ClinicId",
                table: "DoctorClinics");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorClinics_Doctors_DoctorId",
                table: "DoctorClinics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorClinics",
                table: "DoctorClinics");

            migrationBuilder.RenameTable(
                name: "DoctorClinics",
                newName: "DoctorClinic");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorClinics_DoctorId",
                table: "DoctorClinic",
                newName: "IX_DoctorClinic_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorClinics_ClinicId",
                table: "DoctorClinic",
                newName: "IX_DoctorClinic_ClinicId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisteredDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorClinic",
                table: "DoctorClinic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorClinic_Clinics_ClinicId",
                table: "DoctorClinic",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorClinic_Doctors_DoctorId",
                table: "DoctorClinic",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
