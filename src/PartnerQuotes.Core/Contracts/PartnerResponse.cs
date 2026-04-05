namespace PartnerQuotes.Core.Contracts;

/// <summary>Response body for partner read operations.</summary>
public record PartnerResponseDTO(
    /// <summary>Unique identifier of the partner.</summary>
    Guid Id,
    /// <summary>Partner's full name.</summary>
    string Name,
    /// <summary>Partner's email address.</summary>
    string Email,
    /// <summary>Partner's phone number, if provided.</summary>
    string? Phone,
    /// <summary>UTC timestamp when the partner was created.</summary>
    DateTime CreatedAt
);
