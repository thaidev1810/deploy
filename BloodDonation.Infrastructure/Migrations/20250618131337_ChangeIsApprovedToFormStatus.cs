using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIsApprovedToFormStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                schema: "dbo",
                table: "HealthForms");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "dbo",
                table: "HealthForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Pending");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 13, 13, 37, 553, DateTimeKind.Utc).AddTicks(7520));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 13, 13, 37, 553, DateTimeKind.Utc).AddTicks(7520));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 13, 13, 37, 553, DateTimeKind.Utc).AddTicks(7530));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 13, 13, 37, 553, DateTimeKind.Utc).AddTicks(7530));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 13, 13, 37, 553, DateTimeKind.Utc).AddTicks(7530));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 13, 13, 37, 553, DateTimeKind.Utc).AddTicks(7530));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 13, 13, 37, 553, DateTimeKind.Utc).AddTicks(7530));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 13, 13, 37, 553, DateTimeKind.Utc).AddTicks(7530));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "dbo",
                table: "HealthForms");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                schema: "dbo",
                table: "HealthForms",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 11, 10, 22, 19, DateTimeKind.Utc).AddTicks(7040));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 11, 10, 22, 19, DateTimeKind.Utc).AddTicks(7040));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 11, 10, 22, 19, DateTimeKind.Utc).AddTicks(7040));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 11, 10, 22, 19, DateTimeKind.Utc).AddTicks(7050));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 11, 10, 22, 19, DateTimeKind.Utc).AddTicks(7050));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 11, 10, 22, 19, DateTimeKind.Utc).AddTicks(7050));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 11, 10, 22, 19, DateTimeKind.Utc).AddTicks(7050));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 18, 11, 10, 22, 19, DateTimeKind.Utc).AddTicks(7050));
        }
    }
}
