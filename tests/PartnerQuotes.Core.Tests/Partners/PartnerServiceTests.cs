using System.Threading.Tasks;
using Xunit;
using Moq;
using PartnerQuotes.Core.Services;
using PartnerQuotes.Core.Contracts;
using PartnerQuotes.Core.Models;
using PartnerQuotes.Core.Exceptions;

namespace PartnerQuotes.Core.Tests;

public class PartnerServiceTests
{
    [Fact]
    public async Task GetPartnerAsync_ShouldReturnPartner_WhenPartnerExists()
    {
        // Arrange
        var partnerId = Guid.NewGuid();
        var partner = new Partner
        {
            Id = partnerId,
            Name = "Jane Doe",
            Email = "jane@example.com",
            Phone = "555-1234",
            CreatedAt = DateTime.UtcNow
        };
        var mockRepo = new Mock<IPartnerRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(partnerId)).ReturnsAsync(partner);
        var service = new PartnerService(mockRepo.Object);

        // Act
        var result = await service.GetPartnerAsync(partnerId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(partner.Id, result.Id);
        Assert.Equal(partner.Name, result.Name);
        Assert.Equal(partner.Email, result.Email);
        Assert.Equal(partner.Phone, result.Phone);
        Assert.Equal(partner.CreatedAt, result.CreatedAt);
    }

    [Fact]
    public async Task GetPartnerAsync_ShouldThrowPartnerNotFoundException_WhenPartnerDoesNotExist()
    {
        // Arrange
        var partnerId = Guid.NewGuid();
        var mockRepo = new Mock<IPartnerRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(partnerId)).ReturnsAsync((Partner?)null);
        var service = new PartnerService(mockRepo.Object);

        // Act & Assert
        await Assert.ThrowsAsync<PartnerNotFoundException>(() => service.GetPartnerAsync(partnerId));
    }

    [Fact]
    public async Task ListPartnersAsync_ShouldReturnAllPartners_WhenNoFilter()
    {
        // Arrange
        var partners = new List<Partner>
        {
            new Partner { Id = Guid.NewGuid(), Name = "A", Email = "a@example.com", Phone = null, CreatedAt = DateTime.UtcNow },
            new Partner { Id = Guid.NewGuid(), Name = "B", Email = "b@example.com", Phone = "123", CreatedAt = DateTime.UtcNow }
        };
        var mockRepo = new Mock<IPartnerRepository>();
        mockRepo.Setup(r => r.ListAsync(null)).ReturnsAsync(partners);
        var service = new PartnerService(mockRepo.Object);

        // Act
        var result = (await service.ListPartnersAsync()).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(partners[0].Id, result[0].Id);
        Assert.Equal(partners[1].Id, result[1].Id);
    }

    [Fact]
    public async Task ListPartnersAsync_ShouldReturnFilteredPartners_WhenFilterProvided()
    {
        // Arrange
        var partners = new List<Partner>
        {
            new Partner { Id = Guid.NewGuid(), Name = "Alice", Email = "alice@example.com", Phone = null, CreatedAt = DateTime.UtcNow }
        };
        var mockRepo = new Mock<IPartnerRepository>();
        mockRepo.Setup(r => r.ListAsync("Alice")).ReturnsAsync(partners);
        var service = new PartnerService(mockRepo.Object);

        // Act
        var result = (await service.ListPartnersAsync("Alice")).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal("Alice", result[0].Name);
    }

    [Fact]
    public async Task CreatePartnerAsync_ShouldThrowDuplicatePartnerException_WhenEmailExists()
    {
        // Arrange
        var mockRepo = new Mock<IPartnerRepository>();
        mockRepo.Setup(repository => repository.ExistsByEmailAsync(It.IsAny<string>())).ReturnsAsync(true);
        var service = new PartnerService(mockRepo.Object);
        var request = new CreatePartnerRequestDTO("Test User", "duplicate@example.com", "1234567890");

        // Act & Assert
        await Assert.ThrowsAsync<DuplicatePartnerException>(() => service.CreatePartnerAsync(request));
        mockRepo.Verify(repository => repository.CreateAsync(It.IsAny<Partner>()), Times.Never);
    }

    [Fact]
    public async Task CreatePartnerAsync_ShouldCreatePartner_WhenEmailDoesNotExist()
    {
        // Arrange
        var mockRepo = new Mock<IPartnerRepository>();
        mockRepo.Setup(repository => repository.ExistsByEmailAsync(It.IsAny<string>())).ReturnsAsync(false);
        mockRepo.Setup(repository => repository.CreateAsync(It.IsAny<Partner>())).ReturnsAsync((Partner partner) => partner);
        var service = new PartnerService(mockRepo.Object);
        var request = new CreatePartnerRequestDTO("Test User", "test@example.com", "1234567890");

        // Act
        var result = await service.CreatePartnerAsync(request);

        // Assert
        Assert.Equal(request.Name, result.Name);
        Assert.Equal(request.Email, result.Email);
        Assert.Equal(request.Phone, result.Phone);
        Assert.NotEqual(default, result.Id);
        Assert.NotNull(result.ApiKey);
        mockRepo.Verify(repository => repository.CreateAsync(It.IsAny<Partner>()), Times.Once);
    }
}
