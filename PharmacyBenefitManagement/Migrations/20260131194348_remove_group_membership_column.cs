using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyBenefitManagement.Migrations
{
    /// <inheritdoc />
    public partial class remove_group_membership_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupNumber",
                table: "MemberPlans");

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 21, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 22, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 16, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "MemberPlans",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "MemberPlans",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "MemberPlans",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 31, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 43, 47, 592, DateTimeKind.Utc).AddTicks(224));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupNumber",
                table: "MemberPlans",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 17, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 1, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 21, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 22, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 16, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "Claims",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 11, 12, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "MemberPlans",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "GroupNumber" },
                values: new object[] { new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), "GRP-12345" });

            migrationBuilder.UpdateData(
                table: "MemberPlans",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "GroupNumber" },
                values: new object[] { new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), "GRP-67890" });

            migrationBuilder.UpdateData(
                table: "MemberPlans",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "GroupNumber" },
                values: new object[] { new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), null });

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439));

            migrationBuilder.UpdateData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439));
        }
    }
}
