using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sehaty_Plus.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedingIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "019a72b4-22b5-752d-99a9-70ba20a93b20", "019a72b4-22b5-752d-99a9-70bdfa2b942c", false, false, "Admin", "ADMIN" },
                    { "019a72b4-22b5-752d-99a9-70bbe5beed4c", "019a72b4-22b5-752d-99a9-70beb6aceeca", false, false, "Doctor", "DOCTOR" },
                    { "019a72b4-22b5-752d-99a9-70bc36061c72", "019a72b4-22b5-752d-99a9-70bf3e8cc322", false, false, "Patient", "PATIENT" },
                    { "019a72b4-22b6-7d48-ae78-8572c7a26b07", "019a72b4-22b6-7d48-ae78-8573b711cae0", true, false, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Gender", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "RegisteredDate", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "019a72b4-22b3-7297-aed0-a55034375f18", 0, "019a72b4-22b5-752d-99a9-70b93dfe3258", "admin@sehaty-plus.com", true, "Sehaty Plus", 1, true, "Admin", false, null, "ADMIN@SEHATY-PLUS.COM", "ADMIN@SEHATY-PLUS.COM", "AQAAAAIAAYagAAAAENbAD+udZ2X1bEmN/mP4cH0YEEpVaaVq6/5FXf8hys0WsIl1PFMic3ZuU+DfKsvdJQ==", null, false, null, new DateOnly(2025, 11, 12), "019a72b4-22b5-752d-99a9-70b863ac6dae", false, "admin@sehaty-plus.com" });

            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "permissions", "specializations:read", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 2, "permissions", "specializations:add", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 3, "permissions", "specializations:update", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 4, "permissions", "specializations:delete", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 5, "permissions", "specializations:toggle", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 6, "permissions", "patients:read", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 7, "permissions", "patients:details", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 8, "permissions", "patients:add", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 9, "permissions", "patients:update", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 10, "permissions", "patients:delete", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 11, "permissions", "doctors:read", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 12, "permissions", "doctors:details", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 13, "permissions", "doctors:add", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 14, "permissions", "doctors:update", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 15, "permissions", "doctors:delete", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 16, "permissions", "doctors:verify", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 17, "permissions", "clinics:read", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 18, "permissions", "clinics:add", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 19, "permissions", "clinics:update", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 20, "permissions", "clinics:delete", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 21, "permissions", "clinics:toggle", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 22, "permissions", "appointments:read", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 23, "permissions", "appointments:add", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 24, "permissions", "appointments:update", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 25, "permissions", "appointments:cancel", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 26, "permissions", "appointments:confirm", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 27, "permissions", "users:read", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 28, "permissions", "users:add", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 29, "permissions", "users:update", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 30, "permissions", "users:delete", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 31, "permissions", "users:toggle", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 32, "permissions", "roles:read", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 33, "permissions", "roles:add", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 34, "permissions", "roles:update", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 35, "permissions", "roles:delete", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 36, "permissions", "roles:permissions", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 37, "permissions", "reports:read", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 38, "permissions", "reports:export", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 39, "permissions", "analytics:read", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 40, "permissions", "settings:manage", "019a72b4-22b5-752d-99a9-70ba20a93b20" },
                    { 41, "permissions", "logs:read", "019a72b4-22b5-752d-99a9-70ba20a93b20" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "019a72b4-22b5-752d-99a9-70ba20a93b20", "019a72b4-22b3-7297-aed0-a55034375f18" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019a72b4-22b5-752d-99a9-70bbe5beed4c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019a72b4-22b5-752d-99a9-70bc36061c72");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019a72b4-22b6-7d48-ae78-8572c7a26b07");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "019a72b4-22b5-752d-99a9-70ba20a93b20", "019a72b4-22b3-7297-aed0-a55034375f18" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019a72b4-22b5-752d-99a9-70ba20a93b20");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019a72b4-22b3-7297-aed0-a55034375f18");
        }
    }
}
