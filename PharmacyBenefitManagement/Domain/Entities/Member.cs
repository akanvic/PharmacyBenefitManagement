using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmacyBenefitManagement.Domain.Entities
{
    /// <summary>
    /// Represents a healthcare member enrolled in the PBM system
    /// </summary>
    public class Member
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string MemberId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        public bool IsActive { get; set; }

        public DateTime EnrollmentDate { get; set; }

        // Audit fields
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; } = "system";
        public string? ModifiedBy { get; set; }

        // Navigation properties
        public virtual ICollection<MemberPlan> MemberPlans { get; set; } = new List<MemberPlan>();
        public virtual ICollection<Claim> Claims { get; set; } = new List<Claim>();
    }
}
