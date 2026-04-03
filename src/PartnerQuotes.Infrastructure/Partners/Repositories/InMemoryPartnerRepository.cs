using System.Collections.Concurrent;
using PartnerQuotes.Core.Models;
using PartnerQuotes.Core.Services;

namespace PartnerQuotes.Infrastructure.Partners.Repositories;

public class InMemoryPartnerRepository : IPartnerRepository
{
    private readonly ConcurrentDictionary<Guid, Partner> _partners = new();

    public async Task<Partner> CreateAsync(Partner partner)
    {
        _partners[partner.Id] = partner;
        return await Task.FromResult(partner);
    }

    public async Task<Partner?> GetByIdAsync(Guid id)
    {
        _partners.TryGetValue(id, out var partner);
        return await Task.FromResult(partner);
    }

    public async Task<IEnumerable<Partner>> ListAsync(string? nameFilter = null)
    {
        IEnumerable<Partner> query = _partners.Values;

        if (!string.IsNullOrWhiteSpace(nameFilter))
        {
            query = query.Where(p => p.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase));
        }

        return await Task.FromResult(query.OrderBy(p => p.Name));
    }

    public async Task<bool> ExistsByEmailAsync(string email)
    {
        var exists = _partners.Values.Any(p => string.Equals(p.Email, email, StringComparison.OrdinalIgnoreCase));

        return await Task.FromResult(exists);
    }
}
