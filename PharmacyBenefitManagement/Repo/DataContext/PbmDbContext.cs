using Microsoft.EntityFrameworkCore;
using PharmacyBenefitManagement.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PharmacyBenefitManagement.Repo.DataContext
{

    /// <summary>
    /// EF Core database context for PBM system
    /// Configured for performance and healthcare data integrity
    /// </summary>
    public class PbmDbContext : DbContext
    {
        public PbmDbContext(DbContextOptions<PbmDbContext> options) : base(options)
        {
        }

        public DbSet<Member> Members => Set<Member>();
        public DbSet<Plan> Plans => Set<Plan>();
        public DbSet<MemberPlan> MemberPlans => Set<MemberPlan>();
        public DbSet<Claim> Claims => Set<Claim>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Member configuration
            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Members");

                // Unique constraint on business key
                entity.HasIndex(e => e.MemberId)
                    .IsUnique()
                    .HasDatabaseName("IX_Member_MemberId");

                // Composite index for eligibility queries
                // PERFORMANCE: Covers most common query pattern
                entity.HasIndex(e => new { e.MemberId, e.IsActive })
                    .HasDatabaseName("IX_Member_MemberId_IsActive");

                // Soft delete support
                entity.HasQueryFilter(m => m.IsActive || !m.IsActive); // Placeholder for soft delete
            });

            // Plan configuration
            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("Plans");

                entity.HasIndex(e => e.PlanCode)
                    .IsUnique()
                    .HasDatabaseName("IX_Plan_PlanCode");

                entity.HasIndex(e => new { e.PlanCode, e.IsActive })
                    .HasDatabaseName("IX_Plan_PlanCode_IsActive");
            });

            // MemberPlan configuration
            modelBuilder.Entity<MemberPlan>(entity =>
            {
                entity.ToTable("MemberPlans");

                // Composite index for date range queries
                // CRITICAL: Optimizes eligibility lookups by date
                entity.HasIndex(e => new { e.MemberId, e.EffectiveDate, e.TerminationDate })
                    .HasDatabaseName("IX_MemberPlan_MemberId_Dates");

                // Foreign key relationships
                entity.HasOne(mp => mp.Member)
                    .WithMany(m => m.MemberPlans)
                    .HasForeignKey(mp => mp.MemberId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevent cascade deletes

                entity.HasOne(mp => mp.Plan)
                    .WithMany(p => p.MemberPlans)
                    .HasForeignKey(mp => mp.PlanId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Claim configuration
            modelBuilder.Entity<Claim>(entity =>
            {
                entity.ToTable("Claims");

                entity.HasIndex(e => e.ClaimNumber)
                    .IsUnique()
                    .HasDatabaseName("IX_Claim_ClaimNumber");

                // Composite index for recent claims query
                // PERFORMANCE: DESC order for efficient TOP N queries
                entity.HasIndex(e => new { e.MemberId, e.ServiceDate, e.ClaimStatus })
                    .IsDescending(false, true, false) // ServiceDate descending
                    .HasDatabaseName("IX_Claim_MemberId_ServiceDate_Status");

                // Foreign key relationship
                entity.HasOne(c => c.Member)
                    .WithMany(m => m.Claims)
                    .HasForeignKey(c => c.MemberId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Query filter to exclude voided claims by default
                // TRADEOFF: Convenience vs. explicit queries
                entity.HasQueryFilter(c => c.ClaimStatus != "Voided");
            });

            // Seed some reference data
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Seeds initial data for development and testing
        /// </summary>
        private void SeedData(ModelBuilder modelBuilder)
        {
            var now = DateTime.UtcNow;

            // Seed Plans
            modelBuilder.Entity<Plan>().HasData(
                new Plan
                {
                    Id = 1,
                    PlanCode = "PREMIUM-001",
                    PlanName = "Premium Health Plan",
                    PlanType = "PPO",
                    Description = "Comprehensive coverage with nationwide network",
                    Deductible = 500m,
                    OutOfPocketMax = 5000m,
                    EffectiveDate = new DateTime(2024, 1, 1),
                    TerminationDate = null,
                    IsActive = true,
                    CreatedDate = now,
                    CreatedBy = "seed"
                },
                new Plan
                {
                    Id = 2,
                    PlanCode = "BASIC-001",
                    PlanName = "Basic Health Plan",
                    PlanType = "HMO",
                    Description = "Essential coverage with regional network",
                    Deductible = 1000m,
                    OutOfPocketMax = 7000m,
                    EffectiveDate = new DateTime(2024, 1, 1),
                    TerminationDate = null,
                    IsActive = true,
                    CreatedDate = now,
                    CreatedBy = "seed"
                },
                new Plan
                {
                    Id = 3,
                    PlanCode = "MEDICARE-D-001",
                    PlanName = "Medicare Part D Enhanced",
                    PlanType = "Medicare Part D",
                    Description = "Prescription drug coverage for Medicare beneficiaries",
                    Deductible = 250m,
                    OutOfPocketMax = 3000m,
                    EffectiveDate = new DateTime(2024, 1, 1),
                    TerminationDate = null,
                    IsActive = true,
                    CreatedDate = now,
                    CreatedBy = "seed"
                }
            );

            // Seed Members
            modelBuilder.Entity<Member>().HasData(
                new Member
                {
                    Id = 1,
                    MemberId = "M001",
                    FirstName = "John",
                    LastName = "Anderson",
                    DateOfBirth = new DateTime(1975, 5, 15),
                    IsActive = true,
                    EnrollmentDate = new DateTime(2024, 1, 1),
                    CreatedDate = now,
                    CreatedBy = "seed"
                },
                new Member
                {
                    Id = 2,
                    MemberId = "M002",
                    FirstName = "Sarah",
                    LastName = "Williams",
                    DateOfBirth = new DateTime(1982, 8, 22),
                    IsActive = true,
                    EnrollmentDate = new DateTime(2024, 1, 15),
                    CreatedDate = now,
                    CreatedBy = "seed"
                },
                new Member
                {
                    Id = 3,
                    MemberId = "M003",
                    FirstName = "Robert",
                    LastName = "Martinez",
                    DateOfBirth = new DateTime(1945, 3, 10),
                    IsActive = true,
                    EnrollmentDate = new DateTime(2024, 2, 1),
                    CreatedDate = now,
                    CreatedBy = "seed"
                },
                new Member
                {
                    Id = 4,
                    MemberId = "M004",
                    FirstName = "Emily",
                    LastName = "Johnson",
                    DateOfBirth = new DateTime(1990, 11, 5),
                    IsActive = false, // Inactive member for testing
                    EnrollmentDate = new DateTime(2023, 1, 1),
                    CreatedDate = now.AddYears(-1),
                    CreatedBy = "seed"
                }
            );

            // Seed MemberPlans
            modelBuilder.Entity<MemberPlan>().HasData(
                new MemberPlan
                {
                    Id = 1,
                    MemberId = 1,
                    PlanId = 1,
                    EffectiveDate = new DateTime(2024, 1, 1),
                    TerminationDate = new DateTime(2024, 12, 31, 23, 59, 59),
                    RelationshipToSubscriber = "Self",
                    CreatedDate = now,
                    CreatedBy = "seed"
                },
                new MemberPlan
                {
                    Id = 2,
                    MemberId = 2,
                    PlanId = 2,
                    EffectiveDate = new DateTime(2024, 1, 15),
                    TerminationDate = null, // Open-ended coverage
                    RelationshipToSubscriber = "Self",
                    CreatedDate = now,
                    CreatedBy = "seed"
                },
                new MemberPlan
                {
                    Id = 3,
                    MemberId = 3,
                    PlanId = 3,
                    EffectiveDate = new DateTime(2024, 2, 1),
                    TerminationDate = null,
                    RelationshipToSubscriber = "Self",
                    CreatedDate = now,
                    CreatedBy = "seed"
                }
            );

            // Seed Claims
            modelBuilder.Entity<Claim>().HasData(
                // Claims for M001
                new Claim
                {
                    Id = 1,
                    ClaimNumber = "CLM-2024-001",
                    MemberId = 1,
                    ServiceDate = new DateTime(2024, 10, 15),
                    FiledDate = new DateTime(2024, 10, 16),
                    Provider = "City Pharmacy",
                    DrugCode = "00002-7510-01",
                    DrugName = "Lisinopril 10mg",
                    TotalAmount = 150.00m,
                    PaidAmount = 135.00m,
                    MemberResponsibility = 15.00m,
                    ClaimStatus = "Paid",
                    PlanCodeSnapshot = "PREMIUM-001",
                    CreatedDate = now.AddDays(-45),
                    CreatedBy = "system"
                },
                new Claim
                {
                    Id = 2,
                    ClaimNumber = "CLM-2024-002",
                    MemberId = 1,
                    ServiceDate = new DateTime(2024, 11, 1),
                    FiledDate = new DateTime(2024, 11, 2),
                    Provider = "General Hospital Pharmacy",
                    DrugCode = "00378-6110-93",
                    DrugName = "Metformin 500mg",
                    TotalAmount = 85.00m,
                    PaidAmount = 75.00m,
                    MemberResponsibility = 10.00m,
                    ClaimStatus = "Paid",
                    PlanCodeSnapshot = "PREMIUM-001",
                    CreatedDate = now.AddDays(-30),
                    CreatedBy = "system"
                },
                new Claim
                {
                    Id = 3,
                    ClaimNumber = "CLM-2024-003",
                    MemberId = 1,
                    ServiceDate = new DateTime(2024, 11, 20),
                    FiledDate = new DateTime(2024, 11, 21),
                    Provider = "Wellness Pharmacy",
                    DrugCode = "00093-0058-01",
                    DrugName = "Atorvastatin 20mg",
                    TotalAmount = 120.00m,
                    PaidAmount = 0.00m,
                    MemberResponsibility = 0.00m,
                    ClaimStatus = "Pending",
                    PlanCodeSnapshot = "PREMIUM-001",
                    CreatedDate = now.AddDays(-10),
                    CreatedBy = "system"
                },
                // Claims for M002
                new Claim
                {
                    Id = 4,
                    ClaimNumber = "CLM-2024-004",
                    MemberId = 2,
                    ServiceDate = new DateTime(2024, 10, 20),
                    FiledDate = new DateTime(2024, 10, 21),
                    Provider = "Downtown Pharmacy",
                    DrugCode = "59762-0028-01",
                    DrugName = "Amoxicillin 500mg",
                    TotalAmount = 45.00m,
                    PaidAmount = 35.00m,
                    MemberResponsibility = 10.00m,
                    ClaimStatus = "Paid",
                    PlanCodeSnapshot = "BASIC-001",
                    CreatedDate = now.AddDays(-40),
                    CreatedBy = "system"
                },
                new Claim
                {
                    Id = 5,
                    ClaimNumber = "CLM-2024-005",
                    MemberId = 2,
                    ServiceDate = new DateTime(2024, 11, 15),
                    FiledDate = new DateTime(2024, 11, 16),
                    Provider = "Community Health Pharmacy",
                    DrugCode = "00172-5363-70",
                    DrugName = "Synthroid 50mcg",
                    TotalAmount = 95.00m,
                    PaidAmount = 0.00m,
                    MemberResponsibility = 0.00m,
                    ClaimStatus = "Denied",
                    DenialCode = "PA-REQ",
                    DenialReason = "Prior authorization required",
                    PlanCodeSnapshot = "BASIC-001",
                    CreatedDate = now.AddDays(-15),
                    CreatedBy = "system"
                },
                // Claims for M003
                new Claim
                {
                    Id = 6,
                    ClaimNumber = "CLM-2024-006",
                    MemberId = 3,
                    ServiceDate = new DateTime(2024, 9, 10),
                    FiledDate = new DateTime(2024, 9, 11),
                    Provider = "Senior Care Pharmacy",
                    DrugCode = "00781-1506-01",
                    DrugName = "Warfarin 5mg",
                    TotalAmount = 65.00m,
                    PaidAmount = 55.00m,
                    MemberResponsibility = 10.00m,
                    ClaimStatus = "Paid",
                    PlanCodeSnapshot = "MEDICARE-D-001",
                    CreatedDate = now.AddDays(-80),
                    CreatedBy = "system"
                }
            );
        }

        /// <summary>
        /// Override SaveChanges to automatically update audit fields
        /// </summary>
        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        /// <summary>
        /// Override SaveChangesAsync to automatically update audit fields
        /// </summary>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Automatically sets CreatedDate and ModifiedDate for entities
        /// PRODUCTION: Would integrate with authentication to set CreatedBy/ModifiedBy
        /// </summary>
        private void UpdateAuditFields()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Property("CreatedDate").CurrentValue == null ||
                        (DateTime)entry.Property("CreatedDate").CurrentValue == default)
                    {
                        entry.Property("CreatedDate").CurrentValue = DateTime.UtcNow;
                    }
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property("ModifiedDate").CurrentValue = DateTime.UtcNow;
                    // In production, would set ModifiedBy from HttpContext.User
                }
            }
        }
    }

}
