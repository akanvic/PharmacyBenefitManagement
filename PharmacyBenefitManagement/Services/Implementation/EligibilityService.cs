using PharmacyBenefitManagement.Domain.Dtos;
using PharmacyBenefitManagement.Domain.Entities;
using PharmacyBenefitManagement.Exceptions;
using PharmacyBenefitManagement.Repo;
using System.Diagnostics;

namespace PharmacyBenefitManagement.Services.Implementation
{
    public class EligibilityService : IEligibilityService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILogger<EligibilityService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const int RecentClaimsCount = 10;

        public EligibilityService(
            IMemberRepository memberRepository,
            ILogger<EligibilityService> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _memberRepository = memberRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<EligibilitySummaryDto> GetEligibilitySummaryAsync(
            string memberId,
            CancellationToken cancellationToken = default)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(memberId))
            {
                throw new ValidationException("Member ID is required.");
            }

            _logger.LogInformation("Retrieving eligibility summary for member {MemberId}", memberId);

            //Get member
            var member = await _memberRepository.GetMemberByIdAsync(memberId, cancellationToken);

            if (member == null)
            {
                _logger.LogWarning("Member {MemberId} not found", memberId);
                throw new MemberNotFoundException(memberId);
            }

            //Get active plan
            var activeMemberPlan = await _memberRepository.GetActiveMemberPlanAsync(
                member.Id,
                DateTime.UtcNow,
                cancellationToken);

            //Get recent claims
            var recentClaims = await _memberRepository.GetRecentClaimsAsync(
                member.Id,
                RecentClaimsCount,
                cancellationToken);

            //Apply business logic and map to DTO
            var eligibilitySummary = MapToDto(member, activeMemberPlan, recentClaims);

            _logger.LogInformation(
                "Successfully retrieved eligibility summary for member {MemberId}. Eligible: {IsEligible}, Claims: {ClaimCount}",
                memberId,
                eligibilitySummary.IsEligible,
                eligibilitySummary.RecentClaims.Count);

            return eligibilitySummary;
        }


        private EligibilitySummaryDto MapToDto(
            Member member,
            MemberPlan? activeMemberPlan,
            List<Claim> recentClaims)
        {

            var isEligible = member.IsActive && activeMemberPlan != null;

            return new EligibilitySummaryDto
            {
                MemberId = member.MemberId,
                IsEligible = isEligible,
                PlanCode = activeMemberPlan?.Plan?.PlanCode,
                PlanName = activeMemberPlan?.Plan?.PlanName,
                PlanType = activeMemberPlan?.Plan?.PlanType,
                EffectiveDate = activeMemberPlan?.EffectiveDate,
                TerminationDate = activeMemberPlan?.TerminationDate,


                RecentClaims = recentClaims.Select(c => new ClaimDto
                {
                    ClaimNumber = c.ClaimNumber,
                    ServiceDate = c.ServiceDate,
                    Provider = c.Provider,
                    DrugName = c.DrugName,
                    PaidAmount = c.PaidAmount,
                    ClaimStatus = c.ClaimStatus
                }).ToList(),

                Audit = new AuditDto
                {
                    RetrievedAt = DateTime.UtcNow,
                    RetrievedBy = GetCurrentUser(),
                    ApiVersion = "1.0",
                    TraceId = Activity.Current?.Id ?? _httpContextAccessor.HttpContext?.TraceIdentifier
                }
            };
        }

        private string GetCurrentUser()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "system";
        }
    }

}
