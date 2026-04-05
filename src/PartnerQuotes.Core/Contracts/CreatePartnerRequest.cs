using System.ComponentModel.DataAnnotations;

namespace PartnerQuotes.Core.Contracts;

// incoming DTO
public record CreatePartnerRequestDTO(
    [Required]
    [MinLength(1)]
    string Name,
    [Required]
    [EmailAddress]
    [MinLength(1)]
    string Email,
    string? Phone
);
