using System.ComponentModel.DataAnnotations;

namespace PartnerQuotes.Core.Contracts;

/// <summary>Request body for registering a new partner.</summary>
public record CreatePartnerRequestDTO(
    /// <summary>Full name of the partner. Required.</summary>
    [Required]
    [MinLength(1)]
    string Name,
    /// <summary>Partner's email address. Must be unique. Required.</summary>
    [Required]
    [EmailAddress]
    [MinLength(1)]
    string Email,
    /// <summary>Optional phone number.</summary>
    string? Phone
);
