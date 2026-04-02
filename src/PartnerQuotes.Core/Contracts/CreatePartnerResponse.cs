namespace PartnerQuotes.Core.Contracts;

// outgoing DTO for POST create (201 response)
public record CreatePartnerResponseDTO(
    Guid Id,
    string Name,
    string Email,
    string? Phone,
    DateTime CreatedAt,
    string ApiKey
);
