using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InsertDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "BirthDate", "CreateAt", "DocumentNumber", "Email", "FirstName", "LastName", "ManagerName", "PasswordHash", "PasswordSalt", "Role", "UpdateAt" },
                values: new object[] { 1, new DateTime(2000, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 23, 15, 54, 20, 0, DateTimeKind.Unspecified), "123456789", "master_supremo_teste@gmail.com", "Master", "Supremo", "Autônomo", "$2a$11$pZeqWU2G3AptI.TiPfSiHuayRvwsia1QcU.8P9Z.54CbqnfkJmSve", "$2a$11$lkp5CTvlrmBud8RlTnqJie", 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Phone",
                columns: new[] { "Id", "CreateAt", "EmployeeId", "Number", "UpdateAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 24, 3, 6, 41, 111, DateTimeKind.Local).AddTicks(1682), 1, "(31)313131-3131", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 3, 24, 3, 6, 41, 111, DateTimeKind.Local).AddTicks(1683), 1, "+55(11)121212-1212", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Phone",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Phone",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
