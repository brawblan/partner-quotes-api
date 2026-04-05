using Microsoft.AspNetCore.Mvc;
using PartnerQuotes.Core.Contracts;
using PartnerQuotes.Core.Services;
using PartnerQuotes.Core.Exceptions;

namespace PartnerQuotes.Api.Controllers;

/// <summary>
/// Manages partner registration and retrieval.
/// </summary>
[Route("[controller]")]
[ApiController]
public class PartnersController(IPartnerService partnerService) : ControllerBase
{
    /// <summary>
    /// Returns all partners, optionally filtered by name.
    /// </summary>
    /// <param name="nameFilter">Optional partial name to filter by.</param>
    /// <returns>A list of partners.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PartnerResponseDTO>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<PartnerResponseDTO>> GetPartners([FromQuery(Name = "nameFilter")] string? nameFilter = null)
    {
        return await partnerService.ListPartnersAsync(nameFilter);
    }

    /// <summary>
    /// Returns a single partner by ID.
    /// </summary>
    /// <param name="guid">The partner's unique identifier.</param>
    /// <returns>The matching partner, or 404 if not found.</returns>
    [HttpGet("{guid}")]
    [ProducesResponseType(typeof(PartnerResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<PartnerResponseDTO?> GetPartner(Guid guid)
    {
        return await partnerService.GetPartnerAsync(guid);
    }

    /// <summary>
    /// Registers a new partner.
    /// </summary>
    /// <param name="request">Partner registration details.</param>
    /// <returns>The created partner with its API key.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreatePartnerResponseDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreatePartnerRequestDTO request)
    {
        var result = await partnerService.CreatePartnerAsync(request);
        return CreatedAtAction(nameof(GetPartner), new { guid = result.Id }, result);
    }
}
