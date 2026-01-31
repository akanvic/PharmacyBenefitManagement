using PharmacyBenefitManagement.Domain.Dtos;

namespace PharmacyBenefitManagement.Services
{
    public interface IEligibilityService
    {
        Task<EligibilitySummaryDto> GetEligibilitySummaryAsync(string memberId, CancellationToken cancellationToken = default);
    }
}
