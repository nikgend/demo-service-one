using NUnit.Framework;
using Moq;
using TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Repositories;

namespace TEST.AMRCloud.Services.Scoping.Tests.TestMethods.Infrastructure;

/// <summary>
/// Unit tests for Fund Repository.
/// </summary>
[TestFixture]
public class FundRepositoryTests
{
    private Mock<IFundRepository> _mockFundRepository = null!;

    [SetUp]
    public void SetUp()
    {
        _mockFundRepository = new Mock<IFundRepository>();
    }

    [Test]
    public async Task GetFundByCodeAsync_WithValidCode_ReturnsFund()
    {
        // Arrange
        var fundCode = "TF001";
        var fund = new Fund
        {
            Id = 1,
            FundName = "Test Fund",
            FundCode = fundCode,
            Status = "Active"
        };

        _mockFundRepository
            .Setup(r => r.GetFundByCodeAsync(fundCode))
            .ReturnsAsync(fund);

        // Act
        var result = await _mockFundRepository.Object.GetFundByCodeAsync(fundCode);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(fundCode, result.FundCode);
        Assert.AreEqual("Test Fund", result.FundName);
    }

    [Test]
    public async Task GetFundByCodeAsync_WithInvalidCode_ReturnsNull()
    {
        // Arrange
        var fundCode = "INVALID";

        _mockFundRepository
            .Setup(r => r.GetFundByCodeAsync(fundCode))
            .ReturnsAsync((Fund?)null);

        // Act
        var result = await _mockFundRepository.Object.GetFundByCodeAsync(fundCode);

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public async Task GetFundsByStatusAsync_WithActiveStatus_ReturnsFunds()
    {
        // Arrange
        var status = "Active";
        var funds = new[]
        {
            new Fund { Id = 1, FundName = "Fund 1", Status = status },
            new Fund { Id = 2, FundName = "Fund 2", Status = status }
        };

        _mockFundRepository
            .Setup(r => r.GetFundsByStatusAsync(status))
            .ReturnsAsync(funds);

        // Act
        var result = await _mockFundRepository.Object.GetFundsByStatusAsync(status);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
        Assert.IsTrue(result.All(f => f.Status == status));
    }
}

/// <summary>
/// Unit tests for Engagement Repository.
/// </summary>
[TestFixture]
public class EngagementRepositoryTests
{
    private Mock<IEngagementRepository> _mockEngagementRepository = null!;

    [SetUp]
    public void SetUp()
    {
        _mockEngagementRepository = new Mock<IEngagementRepository>();
    }

    [Test]
    public async Task GetEngagementByCodeAsync_WithValidCode_ReturnsEngagement()
    {
        // Arrange
        var engagementCode = "ENG001";
        var engagement = new Engagement
        {
            Id = 1,
            EngagementName = "Test Engagement",
            EngagementCode = engagementCode,
            ClientName = "Test Client"
        };

        _mockEngagementRepository
            .Setup(r => r.GetEngagementByCodeAsync(engagementCode))
            .ReturnsAsync(engagement);

        // Act
        var result = await _mockEngagementRepository.Object.GetEngagementByCodeAsync(engagementCode);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(engagementCode, result.EngagementCode);
        Assert.AreEqual("Test Client", result.ClientName);
    }

    [Test]
    public async Task GetEngagementsByStatusAsync_ReturnsEngagements()
    {
        // Arrange
        var status = "Active";
        var engagements = new[]
        {
            new Engagement { Id = 1, EngagementName = "Engagement 1", Status = status },
            new Engagement { Id = 2, EngagementName = "Engagement 2", Status = status }
        };

        _mockEngagementRepository
            .Setup(r => r.GetEngagementsByStatusAsync(status))
            .ReturnsAsync(engagements);

        // Act
        var result = await _mockEngagementRepository.Object.GetEngagementsByStatusAsync(status);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
    }
}
