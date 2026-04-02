using PartnerQuotes.Core.Contracts;

namespace PartnerQuotes.Core.Services;

public interface IPartnerService
{
    Task<CreatePartnerResponseDTO> CreatePartnerAsync(CreatePartnerRequestDTO request);
    Task<PartnerResponseDTO?> GetPartnerAsync(Guid id);
    Task<IEnumerable<PartnerResponseDTO>> ListPartnersAsync(string? nameFilter = null);
}
