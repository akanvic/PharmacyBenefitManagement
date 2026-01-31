using Microsoft.AspNetCore.Mvc;
using PharmacyBenefitManagement.Domain.Dtos;
using PharmacyBenefitManagement.Services;

namespace PharmacyBenefitManagement.Controllers
{

    [ApiController]
    [Route("api/members")]
    [Produces("application/json")]
    public class MembersController : ControllerBase
    {
        private readonly IEligibilityService _eligibilityService;
        private readonly ILogger<MembersController> _logger;

        public MembersController(
            IEligibilityService eligibilityService,
            ILogger<MembersController> logger)
        {
            _eligibilityService = eligibilityService;
            _logger = logger;
        }


        [HttpGet("{memberId}/eligibility-summary")]
        [ProducesResponseType(typeof(EligibilitySummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EligibilitySummaryDto>> GetEligibilitySummary(
            [FromRoute] string memberId,
            CancellationToken cancellationToken)
        {
            // DESIGN: Minimal controller logic
            // Exception handling delegated to global middleware
            // Business logic delegated to service layer

            _logger.LogDebug("Received eligibility summary request for member {MemberId}", memberId);

            var summary = await _eligibilityService.GetEligibilitySummaryAsync(memberId, cancellationToken);

            return Ok(summary);
        }

    }
}
