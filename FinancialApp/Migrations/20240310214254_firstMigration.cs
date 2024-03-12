using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinancialApp.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "UUID",
                keyValue: new Guid("42c7b4ea-136b-4ff4-ba1e-e22c0a72dbd6"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "UUID",
                keyValue: new Guid("9cf33472-785d-468b-b689-af264e8f0cd8"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "UUID", "AccountName", "Currency", "CustomerId" },
                values: new object[,]
                {
                    { new Guid("906e4f64-6fec-4d38-8ba9-3801e9365790"), "Haluk's Bitcoin Account", "BTC", new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("d791cb4e-9180-4027-9ff9-32f7277e107d"), "Halit's TRY Account", "TRY", new Guid("00000000-0000-0000-0000-000000000001") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "UUID",
                keyValue: new Guid("906e4f64-6fec-4d38-8ba9-3801e9365790"));

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "UUID",
                keyValue: new Guid("d791cb4e-9180-4027-9ff9-32f7277e107d"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "UUID", "AccountName", "Currency", "CustomerId" },
                values: new object[,]
                {
                    { new Guid("42c7b4ea-136b-4ff4-ba1e-e22c0a72dbd6"), "Halit's TRY Account", "TRY", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("9cf33472-785d-468b-b689-af264e8f0cd8"), "Haluk's Bitcoin Account", "BTC", new Guid("00000000-0000-0000-0000-000000000002") }
                });
        }
    }
}
