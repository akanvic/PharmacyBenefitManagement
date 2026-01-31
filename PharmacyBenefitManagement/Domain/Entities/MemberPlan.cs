using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace PharmacyBenefitManagement.Domain.Entities
{
    public class MemberPlan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public int PlanId { get; set; }

        [Required]
        public DateTime EffectiveDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        /// <summary>
        /// Subscriber relationship (Self, Spouse, Dependent)
        /// </summary>
        [StringLength(20)]
        public string RelationshipToSubscriber { get; set; } = "Self";

        // Audit fields
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; } = "system";
        public string? ModifiedBy { get; set; }

        // Navigation properties
        [ForeignKey(nameof(MemberId))]
        public virtual Member Member { get; set; } = null!;

        [ForeignKey(nameof(PlanId))]
        public virtual Plan Plan { get; set; } = null!;
    }
}
