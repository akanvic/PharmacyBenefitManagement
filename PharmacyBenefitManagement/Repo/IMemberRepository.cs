using PharmacyBenefitManagement.Domain.Entities;

namespace PharmacyBenefitManagement.Repo
{
    public interface IMemberRepository
    {
        Task<Member?> GetMemberByIdAsync(string memberId, CancellationToken cancellationToken = default);

        Task<List<Claim>> GetRecentClaimsAsync(int memberId, int count = 10, CancellationToken cancellationToken = default);

        Task<MemberPlan?> GetActiveMemberPlanAsync(int memberId, DateTime? asOfDate = null, CancellationToken cancellationToken = default);
    }
}
