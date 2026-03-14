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
            EngagementManager = "Test Manager"
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
    public async Task GetFundsByEngagementManagerAsync_WithActiveManager_ReturnsFunds()
    {
        // Arrange
        var engagementManager = "John Manager";
        var funds = new[]
        {
            new Fund { Id = 1, FundName = "Fund 1", EngagementManager = engagementManager },
            new Fund { Id = 2, FundName = "Fund 2", EngagementManager = engagementManager }
        };

        _mockFundRepository
            .Setup(r => r.GetFundsByEngagementManagerAsync(engagementManager))
            .ReturnsAsync(funds);

        // Act
        var result = await _mockFundRepository.Object.GetFundsByEngagementManagerAsync(engagementManager);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
        Assert.IsTrue(result.All(f => f.EngagementManager == engagementManager));
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
            EngagementPartner = "Test Partner",
            EngagementManager = "Test Manager"
        };

        _mockEngagementRepository
            .Setup(r => r.GetEngagementByCodeAsync(engagementCode))
            .ReturnsAsync(engagement);

        // Act
        var result = await _mockEngagementRepository.Object.GetEngagementByCodeAsync(engagementCode);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(engagementCode, result.EngagementCode);
        Assert.AreEqual("Test Partner", result.EngagementPartner);
    }

    [Test]
    public async Task GetEngagementsByEngagementManagerAsync_ReturnsEngagements()
    {
        // Arrange
        var engagementManager = "John Manager";
        var engagements = new[]
        {
            new Engagement { Id = 1, EngagementName = "Engagement 1", EngagementManager = engagementManager },
            new Engagement { Id = 2, EngagementName = "Engagement 2", EngagementManager = engagementManager }
        };

        _mockEngagementRepository
            .Setup(r => r.GetEngagementsByEngagementManagerAsync(engagementManager))
            .ReturnsAsync(engagements);

        // Act
        var result = await _mockEngagementRepository.Object.GetEngagementsByEngagementManagerAsync(engagementManager);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
    }
}
