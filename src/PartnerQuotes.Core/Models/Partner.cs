namespace PartnerQuotes.Core.Models;

public class Partner
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public DateTime CreatedAt { get; set; }

}
