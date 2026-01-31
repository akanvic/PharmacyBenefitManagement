using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyBenefitManagement.Domain.Entities
{

    public class Plan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string PlanCode { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string PlanName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        /// <summary>
        /// Type of plan (e.g., HMO, PPO, Medicare Part D)
        /// </summary>
        [Required]
        [StringLength(50)]
        public string PlanType { get; set; } = string.Empty;


        [Column(TypeName = "decimal(18,2)")]
        public decimal Deductible { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal OutOfPocketMax { get; set; }

        public DateTime EffectiveDate { get; set; }


        public DateTime? TerminationDate { get; set; }

        public bool IsActive { get; set; }

        // Audit fields
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; } = "system";
        public string? ModifiedBy { get; set; }

        // Navigation properties
        public virtual ICollection<MemberPlan> MemberPlans { get; set; } = new List<MemberPlan>();
    }

}
