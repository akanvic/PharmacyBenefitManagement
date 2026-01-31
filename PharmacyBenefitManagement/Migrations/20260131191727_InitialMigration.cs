using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PharmacyBenefitManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PlanName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PlanType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Deductible = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OutOfPocketMax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FiledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DrugCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DrugName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MemberResponsibility = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClaimStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DenialCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DenialReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PlanCodeSnapshot = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MemberPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TerminationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GroupNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RelationshipToSubscriber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberPlans_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberPlans_Plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DateOfBirth", "EnrollmentDate", "FirstName", "IsActive", "LastName", "MemberId", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, "seed", new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), new DateTime(1975, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John", true, "Anderson", "M001", null, null },
                    { 2, "seed", new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), new DateTime(1982, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sarah", true, "Williams", "M002", null, null },
                    { 3, "seed", new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), new DateTime(1945, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robert", true, "Martinez", "M003", null, null },
                    { 4, "seed", new DateTime(2025, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), new DateTime(1990, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emily", false, "Johnson", "M004", null, null }
                });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Deductible", "Description", "EffectiveDate", "IsActive", "ModifiedBy", "ModifiedDate", "OutOfPocketMax", "PlanCode", "PlanName", "PlanType", "TerminationDate" },
                values: new object[,]
                {
                    { 1, "seed", new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), 500m, "Comprehensive coverage with nationwide network", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, 5000m, "PREMIUM-001", "Premium Health Plan", "PPO", null },
                    { 2, "seed", new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), 1000m, "Essential coverage with regional network", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, 7000m, "BASIC-001", "Basic Health Plan", "HMO", null },
                    { 3, "seed", new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), 250m, "Prescription drug coverage for Medicare beneficiaries", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, 3000m, "MEDICARE-D-001", "Medicare Part D Enhanced", "Medicare Part D", null }
                });

            migrationBuilder.InsertData(
                table: "Claims",
                columns: new[] { "Id", "ClaimNumber", "ClaimStatus", "CreatedBy", "CreatedDate", "DenialCode", "DenialReason", "DrugCode", "DrugName", "FiledDate", "MemberId", "MemberResponsibility", "ModifiedBy", "ModifiedDate", "PaidAmount", "PlanCodeSnapshot", "Provider", "ServiceDate", "TotalAmount" },
                values: new object[,]
                {
                    { 1, "CLM-2024-001", "Paid", "system", new DateTime(2025, 12, 17, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), null, null, "00002-7510-01", "Lisinopril 10mg", new DateTime(2024, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 15.00m, null, null, 135.00m, "PREMIUM-001", "City Pharmacy", new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 150.00m },
                    { 2, "CLM-2024-002", "Paid", "system", new DateTime(2026, 1, 1, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), null, null, "00378-6110-93", "Metformin 500mg", new DateTime(2024, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 10.00m, null, null, 75.00m, "PREMIUM-001", "General Hospital Pharmacy", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85.00m },
                    { 3, "CLM-2024-003", "Pending", "system", new DateTime(2026, 1, 21, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), null, null, "00093-0058-01", "Atorvastatin 20mg", new DateTime(2024, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0.00m, null, null, 0.00m, "PREMIUM-001", "Wellness Pharmacy", new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 120.00m },
                    { 4, "CLM-2024-004", "Paid", "system", new DateTime(2025, 12, 22, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), null, null, "59762-0028-01", "Amoxicillin 500mg", new DateTime(2024, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 10.00m, null, null, 35.00m, "BASIC-001", "Downtown Pharmacy", new DateTime(2024, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 45.00m },
                    { 5, "CLM-2024-005", "Denied", "system", new DateTime(2026, 1, 16, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), "PA-REQ", "Prior authorization required", "00172-5363-70", "Synthroid 50mcg", new DateTime(2024, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 0.00m, null, null, 0.00m, "BASIC-001", "Community Health Pharmacy", new DateTime(2024, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 95.00m },
                    { 6, "CLM-2024-006", "Paid", "system", new DateTime(2025, 11, 12, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), null, null, "00781-1506-01", "Warfarin 5mg", new DateTime(2024, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 10.00m, null, null, 55.00m, "MEDICARE-D-001", "Senior Care Pharmacy", new DateTime(2024, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 65.00m }
                });

            migrationBuilder.InsertData(
                table: "MemberPlans",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "EffectiveDate", "GroupNumber", "MemberId", "ModifiedBy", "ModifiedDate", "PlanId", "RelationshipToSubscriber", "TerminationDate" },
                values: new object[,]
                {
                    { 1, "seed", new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GRP-12345", 1, null, null, 1, "Self", new DateTime(2024, 12, 31, 23, 59, 59, 0, DateTimeKind.Unspecified) },
                    { 2, "seed", new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "GRP-67890", 2, null, null, 2, "Self", null },
                    { 3, "seed", new DateTime(2026, 1, 31, 19, 17, 26, 583, DateTimeKind.Utc).AddTicks(5439), new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, null, null, 3, "Self", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claim_ClaimNumber",
                table: "Claims",
                column: "ClaimNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Claim_MemberId_ServiceDate_Status",
                table: "Claims",
                columns: new[] { "MemberId", "ServiceDate", "ClaimStatus" },
                descending: new[] { false, true, false });

            migrationBuilder.CreateIndex(
                name: "IX_MemberPlan_MemberId_Dates",
                table: "MemberPlans",
                columns: new[] { "MemberId", "EffectiveDate", "TerminationDate" });

            migrationBuilder.CreateIndex(
                name: "IX_MemberPlans_PlanId",
                table: "MemberPlans",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Member_MemberId",
                table: "Members",
                column: "MemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Member_MemberId_IsActive",
                table: "Members",
                columns: new[] { "MemberId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_Plan_PlanCode",
                table: "Plans",
                column: "PlanCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plan_PlanCode_IsActive",
                table: "Plans",
                columns: new[] { "PlanCode", "IsActive" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "MemberPlans");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Plans");
        }
    }
}
