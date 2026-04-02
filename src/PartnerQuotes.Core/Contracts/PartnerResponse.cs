namespace PartnerQuotes.Core.Contracts;

// outgoing DTO for GET reads
public record PartnerResponseDTO(
    Guid Id,
    string Name,
    string Email,
    string? Phone,
    DateTime CreatedAt
);
