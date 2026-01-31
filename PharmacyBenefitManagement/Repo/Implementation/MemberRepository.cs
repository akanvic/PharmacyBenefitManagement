using Microsoft.EntityFrameworkCore;
using PharmacyBenefitManagement.Domain.Entities;
using PharmacyBenefitManagement.Repo.DataContext;

namespace PharmacyBenefitManagement.Repo.Implementation
{

    public class MemberRepository : IMemberRepository
    {
        private readonly PbmDbContext _context;

        public MemberRepository(PbmDbContext context)
        {
            _context = context;
        }


        public async Task<Member?> GetMemberByIdAsync(string memberId, CancellationToken cancellationToken = default)
        {
            return await _context.Members
                .AsNoTracking() 
                .FirstOrDefaultAsync(m => m.MemberId == memberId, cancellationToken);
        }

        public async Task<List<Claim>> GetRecentClaimsAsync(
            int memberId,
            int count = 10,
            CancellationToken cancellationToken = default)
        {
            return await _context.Claims
                .AsNoTracking()
                .Where(c => c.MemberId == memberId)
                .Where(c => c.ClaimStatus != "Voided") // Explicit filter (though query filter also applies)
                .OrderByDescending(c => c.ServiceDate) // Uses IX_Claim_MemberId_ServiceDate_Status
                .Take(count) 
                .ToListAsync(cancellationToken);
        }

        public async Task<MemberPlan?> GetActiveMemberPlanAsync(
            int memberId,
            DateTime? asOfDate = null,
            CancellationToken cancellationToken = default)
        {
            var checkDate = asOfDate ?? DateTime.UtcNow;

            return await _context.MemberPlans
                .AsNoTracking()
                .Include(mp => mp.Plan) // EAGER LOAD: Prevents N+1, single round-trip
                .Where(mp => mp.MemberId == memberId)
                .Where(mp => mp.EffectiveDate <= checkDate)
                .Where(mp => mp.TerminationDate == null || mp.TerminationDate >= checkDate)
                .OrderByDescending(mp => mp.EffectiveDate) 
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
