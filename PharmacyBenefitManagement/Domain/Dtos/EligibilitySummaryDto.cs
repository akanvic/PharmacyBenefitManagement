namespace PharmacyBenefitManagement.Domain.Dtos
{
    public class EligibilitySummaryDto
    {

        public string MemberId { get; set; } = string.Empty;

        public bool IsEligible { get; set; }

        public string? PlanCode { get; set; }

        public string? PlanName { get; set; }

        public string? PlanType { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public List<ClaimDto> RecentClaims { get; set; } = new();

        public AuditDto Audit { get; set; } = new();
    }

}
