using PartnerQuotes.Core.Models;

namespace PartnerQuotes.Core.Services;

public interface IPartnerRepository
{
    Task<Partner> CreateAsync(Partner partner);
    Task<Partner?> GetByIdAsync(Guid id);
    Task<IEnumerable<Partner>> ListAsync(string? nameFilter = null);
    Task<bool> ExistsByEmailAsync(string email);
}
