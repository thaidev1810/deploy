using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BloodDonation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameAmountAndAddComponentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AmountNeeded",
                schema: "dbo",
                table: "DonationRequests",
                newName: "AmountBlood");

            migrationBuilder.AddColumn<string>(
                name: "ComponentType",
                schema: "dbo",
                table: "DonationRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComponentType",
                schema: "dbo",
                table: "DonationRequests");

            migrationBuilder.RenameColumn(
                name: "AmountBlood",
                schema: "dbo",
                table: "DonationRequests",
                newName: "AmountNeeded");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 14, 20, 45, 10, 546, DateTimeKind.Utc).AddTicks(4450));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 14, 20, 45, 10, 546, DateTimeKind.Utc).AddTicks(4457));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 14, 20, 45, 10, 546, DateTimeKind.Utc).AddTicks(4458));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 14, 20, 45, 10, 546, DateTimeKind.Utc).AddTicks(4460));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 14, 20, 45, 10, 546, DateTimeKind.Utc).AddTicks(4461));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 14, 20, 45, 10, 546, DateTimeKind.Utc).AddTicks(4463));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 14, 20, 45, 10, 546, DateTimeKind.Utc).AddTicks(4465));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "BloodStored",
                keyColumn: "StoredId",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                column: "LastUpdated",
                value: new DateTime(2025, 6, 14, 20, 45, 10, 546, DateTimeKind.Utc).AddTicks(4466));
        }
    }
}
