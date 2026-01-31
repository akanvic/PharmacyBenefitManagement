using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyBenefitManagement.Domain.Entities
{
    /// <summary>
    /// Represents a pharmacy claim submitted for a member
    /// </summary>
    public class Claim
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ClaimNumber { get; set; } = string.Empty;

        [Required]
        public int MemberId { get; set; }

        /// <summary>
        /// Date when the service was provided
        /// </summary>
        [Required]
        public DateTime ServiceDate { get; set; }

        [Required]
        public DateTime FiledDate { get; set; }

        /// <summary>
        /// Pharmacy or provider name
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Provider { get; set; } = string.Empty;

        /// <summary>
        /// National Drug Code (NDC)
        /// </summary>
        [StringLength(20)]
        public string? DrugCode { get; set; }

        [StringLength(200)]
        public string? DrugName { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal PaidAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal MemberResponsibility { get; set; }

        /// <summary>
        /// Claim status: Paid, Pending, Denied, Voided
        /// </summary>
        [Required]
        [StringLength(20)]
        public string ClaimStatus { get; set; } = "Pending";

        /// <summary>
        /// Denial reason code if applicable
        /// </summary>
        [StringLength(10)]
        public string? DenialCode { get; set; }

        [StringLength(500)]
        public string? DenialReason { get; set; }

        /// <summary>
        /// Plan code snapshot at time of claim (for audit trail)
        /// </summary>
        [StringLength(50)]
        public string? PlanCodeSnapshot { get; set; }

        // Audit fields
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; } = "system";
        public string? ModifiedBy { get; set; }

        // Navigation properties
        [ForeignKey(nameof(MemberId))]
        public virtual Member Member { get; set; } = null!;
    }
}
