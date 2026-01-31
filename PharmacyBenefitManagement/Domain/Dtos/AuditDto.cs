namespace PharmacyBenefitManagement.Domain.Dtos
{
    public class AuditDto
    {

        public DateTime RetrievedAt { get; set; }

        public string RetrievedBy { get; set; } = "system";

        public string ApiVersion { get; set; } = "1.0";

        public string? TraceId { get; set; }
    }
}
