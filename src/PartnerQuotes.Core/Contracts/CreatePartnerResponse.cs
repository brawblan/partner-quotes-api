namespace PartnerQuotes.Core.Contracts;

/// <summary>Response body returned after successfully registering a new partner.</summary>
public record CreatePartnerResponseDTO(
    /// <summary>Unique identifier assigned to the partner.</summary>
    Guid Id,
    /// <summary>Partner's full name.</summary>
    string Name,
    /// <summary>Partner's email address.</summary>
    string Email,
    /// <summary>Partner's phone number, if provided.</summary>
    string? Phone,
    /// <summary>UTC timestamp when the partner was created.</summary>
    DateTime CreatedAt,
    /// <summary>Generated API key for the partner.</summary>
    string ApiKey
);
