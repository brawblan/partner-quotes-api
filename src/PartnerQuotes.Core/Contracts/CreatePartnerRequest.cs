namespace PartnerQuotes.Core.Contracts;

// incoming DTO
public record CreatePartnerRequestDTO(
    string Name,
    string Email,
    string? Phone
);
