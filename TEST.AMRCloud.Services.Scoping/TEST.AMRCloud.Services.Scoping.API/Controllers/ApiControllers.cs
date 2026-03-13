using MediatR;
using Microsoft.AspNetCore.Mvc;
using TEST.AMRCloud.Services.Scoping.Application.DbOps;
using TEST.AMRCloud.Services.Scoping.Domain.Contracts;
namespace TEST.AMRCloud.Services.Scoping.API.Controllers;

/// <summary>
/// Engagement API Controller.
/// Provides endpoints for engagement-related operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EngagementController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<EngagementController> _logger;

    public EngagementController(IMediator mediator, ILogger<EngagementController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get engagement by ID.
    /// </summary>
    /// <param name="id">Engagement ID</param>
    /// <returns>Engagement details</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEngagement(int id)
    {
        try
        {
            _logger.LogInformation("Retrieving engagement with ID: {EngagementId}", id);

            var query = new GetEngagementByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogWarning("Engagement with ID {EngagementId} not found", id);
                return NotFound($"Engagement with ID {id} not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving engagement with ID: {EngagementId}", id);
            return StatusCode(500, "An error occurred while retrieving the engagement");
        }
    }

    /// <summary>
    /// Get all engagements.
    /// </summary>
    /// <returns>List of all engagements</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllEngagements()
    {
        try
        {
            _logger.LogInformation("Retrieving all engagements");

            var query = new GetAllEngagementsQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all engagements");
            return StatusCode(500, "An error occurred while retrieving engagements");
        }
    }

    /// <summary>
    /// Get engagements by status.
    /// </summary>
    /// <param name="status">Engagement status</param>
    /// <returns>List of engagements with the specified status</returns>
    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetEngagementsByStatus(string status)
    {
        try
        {
            _logger.LogInformation("Retrieving engagements with status: {Status}", status);

            var query = new GetEngagementsByStatusQuery(status);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving engagements with status: {Status}", status);
            return StatusCode(500, "An error occurred while retrieving engagements");
        }
    }

    /// <summary>
    /// Get engagement by code.
    /// </summary>
    /// <param name="code">Engagement code</param>
    /// <returns>Engagement details</returns>
    [HttpGet("code/{code}")]
    public async Task<IActionResult> GetEngagementByCode(string code)
    {
        try
        {
            _logger.LogInformation("Retrieving engagement with code: {Code}", code);

            var query = new GetEngagementByCodeQuery(code);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogWarning("Engagement with code {Code} not found", code);
                return NotFound($"Engagement with code {code} not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving engagement with code: {Code}", code);
            return StatusCode(500, "An error occurred while retrieving the engagement");
        }
    }

    /// <summary>
    /// Get engagement with its associated funds.
    /// </summary>
    /// <param name="id">Engagement ID</param>
    /// <returns>Engagement details with associated funds</returns>
    [HttpGet("{id}/funds")]
    public async Task<IActionResult> GetEngagementWithFunds(int id)
    {
        try
        {
            _logger.LogInformation("Retrieving engagement with funds for ID: {EngagementId}", id);

            var query = new GetEngagementWithFundsQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                _logger.LogWarning("Engagement with ID {EngagementId} not found", id);
                return NotFound($"Engagement with ID {id} not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving engagement with funds for ID: {EngagementId}", id);
            return StatusCode(500, "An error occurred while retrieving the engagement");
        }
    }

    /// <summary>
    /// Create a new engagement.
    /// </summary>
    /// <param name="engagementName">Engagement name</param>
    /// <param name="engagementCode">Engagement code</param>
    /// <param name="clientName">Client name</param>
    /// <param name="status">Engagement status</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <param name="createdBy">Created by user</param>
    /// <returns>Created engagement details</returns>
    [HttpPost]
    public async Task<IActionResult> CreateEngagement([FromBody] EngagementDto engagementDto)
    {
        try
        {
            _logger.LogInformation("Creating engagement: {EngagementCode}", engagementDto.EngagementCode);

            var command = new CreateEngagementCommand(engagementDto);
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogError("Failed to create engagement: {EngagementCode}", engagementDto.EngagementCode);
                return StatusCode(500, "An error occurred while creating the engagement");
            }

            return CreatedAtAction(nameof(GetEngagement), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating engagement: {EngagementCode}", engagementDto.EngagementCode);
            return StatusCode(500, "An error occurred while creating the engagement");
        }
    }

    /// <summary>
    /// Update an existing engagement.
    /// </summary>
    /// <param name="id">Engagement ID</param>
    /// <param name="engagementName">Engagement name</param>
    /// <param name="engagementCode">Engagement code</param>
    /// <param name="clientName">Client name</param>
    /// <param name="status">Engagement status</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <param name="modifiedBy">Modified by user</param>
    /// <returns>Updated engagement details</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEngagement([FromBody] EngagementDto engagementDto)
    {
        try
        {
            _logger.LogInformation("Updating engagement with ID: {EngagementId}", engagementDto.Id);

            var command = new UpdateEngagementCommand(engagementDto);
            var result = await _mediator.Send(command);

            if (result == null)
            {
                _logger.LogWarning("Engagement with ID {EngagementId} not found", engagementDto.Id);
                return NotFound($"Engagement with ID {engagementDto.Id} not found");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating engagement with ID: {EngagementId}", engagementDto.Id);
            return StatusCode(500, "An error occurred while updating the engagement");
        }
    }

    /// <summary>
    /// Delete an engagement.
    /// </summary>
    /// <param name="id">Engagement ID</param>
    /// <returns>Success or failure result</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEngagement(int id)
    {
        try
        {
            _logger.LogInformation("Deleting engagement with ID: {EngagementId}", id);

            var command = new DeleteEngagementCommand(id);
            var result = await _mediator.Send(command);

            if (!result)
            {
                _logger.LogWarning("Engagement with ID {EngagementId} not found", id);
                return NotFound($"Engagement with ID {id} not found");
            }

            _logger.LogInformation("Engagement with ID {EngagementId} deleted successfully", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting engagement with ID: {EngagementId}", id);
            return StatusCode(500, "An error occurred while deleting the engagement");
        }
    }
}



