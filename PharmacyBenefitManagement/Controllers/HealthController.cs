using Microsoft.AspNetCore.Mvc;
using PharmacyBenefitManagement.Repo.DataContext;

namespace PharmacyBenefitManagement.Controllers
{

    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        private readonly PbmDbContext _context;
        private readonly ILogger<HealthController> _logger;

        public HealthController(PbmDbContext context, ILogger<HealthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(HealthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HealthResponse), StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<HealthResponse>> Get()
        {
            var response = new HealthResponse
            {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow,
                Version = "1.0"
            };

            try
            {
                // Check database connectivity
                var canConnect = await _context.Database.CanConnectAsync();
                response.DatabaseStatus = canConnect ? "Healthy" : "Unhealthy";

                if (!canConnect)
                {
                    response.Status = "Unhealthy";
                    _logger.LogError("Health check failed: Database connection unavailable");
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Health check failed with exception");
                response.Status = "Unhealthy";
                response.DatabaseStatus = "Error";
                return StatusCode(StatusCodes.Status503ServiceUnavailable, response);
            }
        }


        [HttpGet("detailed")]
        [ProducesResponseType(typeof(DetailedHealthResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<DetailedHealthResponse>> GetDetailed()
        {
            var response = new DetailedHealthResponse
            {
                Status = "Healthy",
                Timestamp = DateTime.UtcNow,
                Version = "1.0"
            };

            // Check database
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();
                response.Components["database"] = new ComponentHealth
                {
                    Status = canConnect ? "Healthy" : "Unhealthy",
                    ResponseTime = 0 // Would measure actual response time in production
                };
            }
            catch (Exception ex)
            {
                response.Components["database"] = new ComponentHealth
                {
                    Status = "Unhealthy",
                    Error = ex.Message
                };
                response.Status = "Degraded";
            }


            return Ok(response);
        }
    }

    public class HealthResponse
    {
        public string Status { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string Version { get; set; } = string.Empty;
        public string DatabaseStatus { get; set; } = string.Empty;
    }

    public class DetailedHealthResponse
    {
        public string Status { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string Version { get; set; } = string.Empty;
        public Dictionary<string, ComponentHealth> Components { get; set; } = new();
    }

    public class ComponentHealth
    {
        public string Status { get; set; } = string.Empty;
        public double ResponseTime { get; set; }
        public string? Error { get; set; }
    }

}
