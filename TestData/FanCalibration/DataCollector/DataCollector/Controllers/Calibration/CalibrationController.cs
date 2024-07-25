using System.Net;
using DataCollector.Controllers.Calibration.Extensions;
using DataCollector.Controllers.Calibration.Models;
using DataCollector.Models;
using DataCollector.Repository.Contracts.Calibration;
using DataCollector.Repository.Contracts.Calibration.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataCollector.Controllers.Calibration;

[ApiController]
[Route("api/[controller]")]
public class CalibrationController(ILogger<CalibrationController> logger, ICalibrationRepository calibrationRepository) : ControllerBase
{
    /// <summary>
    ///     Returns a list of all fans
    /// </summary>
    /// <returns></returns>
    [HttpGet("Fans")]
    [ProducesResponseType<ListResponse<FanPreviewItem>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRegisteredFansAsync()
    {
        try
        {
            logger.LogInformation("Trying to get a list of all registered fans.");
            var fans = await calibrationRepository.GetFansAsync().ConfigureAwait(false);
            logger.LogInformation("Found {fanCount} fans in the registry.", fans.Length);

            var response = ListResponse<FanPreviewItem>.FromSequence(fans, entity => entity.Map());
            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "An unexpected error occurred while trying to load the list of registered fans.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    ///     Creates a new fan registration
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("Fans")]
    [ProducesResponseType<FanPreviewItem>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateFanRegistrationAsync([FromBody] CreateFanRequest request)
    {
        try
        {
            logger.LogInformation("Trying to create a new fan registration with the name '{fanName}'", request.Name);

            var isNameMissing = string.IsNullOrWhiteSpace(request.Name);
            if (isNameMissing)
            {
                logger.LogWarning("A user tried to create a fan, but failed to provide a name for it.");
                return BadRequest();
            }
            
            var isFanAlreadyRegistered = await calibrationRepository
                    .CheckWhetherFanExistsAsync(request.Name)
                    .ConfigureAwait(false);
            if (isFanAlreadyRegistered)
            {
                logger.LogWarning("A user tried to create a fan with a name that already exists.");
                return BadRequest();
            }

            logger.LogInformation("The request to create a registration passed validation.");
            
            var fan = new FanEntity
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };
            await calibrationRepository.RegisterFanAsync(fan).ConfigureAwait(false);
            logger.LogInformation("The fan has been registered successfully.");

            return Ok(fan.Map());
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "An unexpected error occurred while trying to create a fan registration.");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}