using Microsoft.AspNetCore.Mvc;
using PartnerQuotes.Core.Contracts;
using PartnerQuotes.Core.Services;
using PartnerQuotes.Core.Exceptions;

namespace PartnerQuotes.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class PartnersController(IPartnerService partnerService) : ControllerBase
{
    // GET: Partners?nameFilter=asdf
    [HttpGet]
    public async Task<IEnumerable<PartnerResponseDTO>> GetPartners([FromQuery(Name = "nameFilter")] string? nameFilter = null)
    {
        return await partnerService.ListPartnersAsync(nameFilter);
    }

    // GET: Partners/guid
    [HttpGet("{guid}")]
    public async Task<PartnerResponseDTO?> GetPartner(Guid guid)
    {
        return await partnerService.GetPartnerAsync(guid);
    }

    // POST: Create Partner
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePartnerRequestDTO request)
    {
        var result = await partnerService.CreatePartnerAsync(request);
        return CreatedAtAction(nameof(GetPartner), new { guid = result.Id }, result);
    }
}
