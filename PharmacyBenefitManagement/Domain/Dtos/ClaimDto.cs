namespace PharmacyBenefitManagement.Domain.Dtos
{
    public class ClaimDto
    {
        public string ClaimNumber { get; set; } = string.Empty;
        public DateTime ServiceDate { get; set; }
        public string Provider { get; set; } = string.Empty;
        public string? DrugName { get; set; }
        public decimal PaidAmount { get; set; }
        public string ClaimStatus { get; set; } = string.Empty;
    }
}
