using PartnerQuotes.Core.Contracts;
using PartnerQuotes.Core.Models;
using PartnerQuotes.Core.Exceptions;

using System;
using System.Security.Cryptography;

namespace PartnerQuotes.Core.Services;

public class PartnerService(IPartnerRepository repository) : IPartnerService
{
    private readonly IPartnerRepository _repository = repository;

    public async Task<CreatePartnerResponseDTO> CreatePartnerAsync(CreatePartnerRequestDTO request)
    {

        if (await _repository.ExistsByEmailAsync(request.Email.Trim()))
        {
            throw new DuplicatePartnerException($"Email {request.Email} already exists.");
        }

        Partner newPartner = new Partner
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Email = request.Email.Trim(),
            Phone = string.IsNullOrWhiteSpace(request.Phone) ? null : request.Phone.Trim(),
            CreatedAt = DateTime.UtcNow
        };
        string apiKey = GenerateApiKey();
        await _repository.CreateAsync(newPartner);
        return new CreatePartnerResponseDTO(
            newPartner.Id,
            newPartner.Name,
            newPartner.Email,
            newPartner.Phone,
            newPartner.CreatedAt,
            apiKey
        );
    }

    public async Task<PartnerResponseDTO?> GetPartnerAsync(Guid id)
    {
        Partner partner = await _repository.GetByIdAsync(id) ??
            throw new PartnerNotFoundException($"Partner with id: {id} not found.");
        return new PartnerResponseDTO(
                partner.Id,
                partner.Name,
                partner.Email,
                partner.Phone,
                partner.CreatedAt
            );
    }

    public async Task<IEnumerable<PartnerResponseDTO>> ListPartnersAsync(string? nameFilter = null)
    {
        var partners = await _repository.ListAsync(nameFilter);
        return partners.Select(partner => new PartnerResponseDTO(
            partner.Id,
            partner.Name,
            partner.Email,
            partner.Phone,
            partner.CreatedAt
        ));
    }

    private static string GenerateApiKey(int length = 32)
    {
        var bytes = new byte[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(bytes);
        }
        // Convert to Base64 or Hex for a string representation
        return Convert.ToBase64String(bytes)
            .Replace("/", "")
            .Replace("+", "")
            .Replace("=", ""); // Optional: Clean up for URL-friendliness
    }
}
