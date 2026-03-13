using NUnit.Framework;
using Moq;
using TEST.AMRCloud.Services.Scoping.Application.DbOps;
using TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Repositories;

namespace TEST.AMRCloud.Services.Scoping.Tests.TestMethods.Application;

/// <summary>
/// Unit tests for Fund Query Handlers.
/// Tests the query handler logic and DTO mapping.
/// </summary>
[TestFixture]
public class FundQueryHandlerTests
{
    private Mock<IFundRepository> _mockFundRepository = null!;
    private GetFundByIdQueryHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _mockFundRepository = new Mock<IFundRepository>();
        _handler = new GetFundByIdQueryHandler(_mockFundRepository.Object);
    }

    [Test]
    public async Task Handle_WithValidFundId_ReturnsFundDto()
    {
        // Arrange
        var fundId = 1;
        var fund = new Fund
        {
            Id = fundId,
            FundName = "Test Fund",
            FundCode = "TF001",
            Status = "Active",
            Amount = 1000000,
            CreatedDate = DateTime.Now,
            CreatedBy = "TestUser"
        };

        _mockFundRepository
            .Setup(r => r.GetByIdAsync(fundId))
            .ReturnsAsync(fund);

        var query = new GetFundByIdQuery(fundId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(fundId, result.Id);
        Assert.AreEqual("Test Fund", result.FundName);
        Assert.AreEqual("TF001", result.FundCode);
        Assert.AreEqual("Active", result.Status);
        Assert.AreEqual(1000000, result.Amount);

        _mockFundRepository.Verify(r => r.GetByIdAsync(fundId), Times.Once);
    }

    [Test]
    public async Task Handle_WithInvalidFundId_ReturnsNull()
    {
        // Arrange
        var fundId = 999;
        _mockFundRepository
            .Setup(r => r.GetByIdAsync(fundId))
            .ReturnsAsync((Fund?)null);

        var query = new GetFundByIdQuery(fundId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNull(result);
        _mockFundRepository.Verify(r => r.GetByIdAsync(fundId), Times.Once);
    }
}

/// <summary>
/// Unit tests for Engagement Query Handler.
/// </summary>
[TestFixture]
public class EngagementQueryHandlerTests
{
    private Mock<IEngagementRepository> _mockEngagementRepository = null!;
    private GetEngagementByIdQueryHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _mockEngagementRepository = new Mock<IEngagementRepository>();
        _handler = new GetEngagementByIdQueryHandler(_mockEngagementRepository.Object);
    }

    [Test]
    public async Task Handle_WithValidEngagementId_ReturnsEngagementDto()
    {
        // Arrange
        var engagementId = 1;
        var engagement = new Engagement
        {
            Id = engagementId,
            EngagementName = "Test Engagement",
            EngagementCode = "ENG001",
            ClientName = "Test Client",
            Status = "Active",
            StartDate = DateTime.Now.AddMonths(-1),
            EndDate = DateTime.Now.AddMonths(1),
            CreatedDate = DateTime.Now,
            CreatedBy = "TestUser"
        };

        _mockEngagementRepository
            .Setup(r => r.GetByIdAsync(engagementId))
            .ReturnsAsync(engagement);

        var query = new GetEngagementByIdQuery(engagementId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(engagementId, result.Id);
        Assert.AreEqual("Test Engagement", result.EngagementName);
        Assert.AreEqual("Test Client", result.ClientName);

        _mockEngagementRepository.Verify(r => r.GetByIdAsync(engagementId), Times.Once);
    }
}

/// <summary>
/// Unit tests for GetAllEngagements Query Handler.
/// </summary>
[TestFixture]
public class GetAllEngagementsQueryHandlerTests
{
    private Mock<IEngagementRepository> _mockEngagementRepository = null!;
    private GetAllEngagementsQueryHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _mockEngagementRepository = new Mock<IEngagementRepository>();
        _handler = new GetAllEngagementsQueryHandler(_mockEngagementRepository.Object);
    }

    [Test]
    public async Task Handle_ReturnsAllEngagements()
    {
        // Arrange
        var engagements = new List<Engagement>
        {
            new Engagement
            {
                Id = 1,
                EngagementName = "Engagement 1",
                EngagementCode = "ENG001",
                ClientName = "Client 1",
                Status = "Active",
                CreatedDate = DateTime.Now,
                CreatedBy = "TestUser"
            },
            new Engagement
            {
                Id = 2,
                EngagementName = "Engagement 2",
                EngagementCode = "ENG002",
                ClientName = "Client 2",
                Status = "Completed",
                CreatedDate = DateTime.Now,
                CreatedBy = "TestUser"
            }
        };

        _mockEngagementRepository
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(engagements);

        var query = new GetAllEngagementsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
        Assert.AreEqual("Engagement 1", result.First().EngagementName);

        _mockEngagementRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Test]
    public async Task Handle_WithNoEngagements_ReturnsEmptyList()
    {
        // Arrange
        _mockEngagementRepository
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Engagement>());

        var query = new GetAllEngagementsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count());

        _mockEngagementRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }
}

/// <summary>
/// Unit tests for GetEngagementsByStatus Query Handler.
/// </summary>
[TestFixture]
public class GetEngagementsByStatusQueryHandlerTests
{
    private Mock<IEngagementRepository> _mockEngagementRepository = null!;
    private GetEngagementsByStatusQueryHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _mockEngagementRepository = new Mock<IEngagementRepository>();
        _handler = new GetEngagementsByStatusQueryHandler(_mockEngagementRepository.Object);
    }

    [Test]
    public async Task Handle_WithValidStatus_ReturnsEngagementsWithStatus()
    {
        // Arrange
        var status = "Active";
        var engagements = new List<Engagement>
        {
            new Engagement
            {
                Id = 1,
                EngagementName = "Engagement 1",
                EngagementCode = "ENG001",
                Status = status,
                CreatedDate = DateTime.Now,
                CreatedBy = "TestUser"
            }
        };

        _mockEngagementRepository
            .Setup(r => r.GetEngagementsByStatusAsync(status))
            .ReturnsAsync(engagements);

        var query = new GetEngagementsByStatusQuery(status);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual(status, result.First().Status);

        _mockEngagementRepository.Verify(r => r.GetEngagementsByStatusAsync(status), Times.Once);
    }

    [Test]
    public async Task Handle_WithNoMatchingStatus_ReturnsEmptyList()
    {
        // Arrange
        var status = "NonExistent";
        _mockEngagementRepository
            .Setup(r => r.GetEngagementsByStatusAsync(status))
            .ReturnsAsync(new List<Engagement>());

        var query = new GetEngagementsByStatusQuery(status);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(0, result.Count());

        _mockEngagementRepository.Verify(r => r.GetEngagementsByStatusAsync(status), Times.Once);
    }
}

/// <summary>
/// Unit tests for GetEngagementByCode Query Handler.
/// </summary>
[TestFixture]
public class GetEngagementByCodeQueryHandlerTests
{
    private Mock<IEngagementRepository> _mockEngagementRepository = null!;
    private GetEngagementByCodeQueryHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _mockEngagementRepository = new Mock<IEngagementRepository>();
        _handler = new GetEngagementByCodeQueryHandler(_mockEngagementRepository.Object);
    }

    [Test]
    public async Task Handle_WithValidCode_ReturnsEngagementDto()
    {
        // Arrange
        var code = "ENG001";
        var engagement = new Engagement
        {
            Id = 1,
            EngagementName = "Test Engagement",
            EngagementCode = code,
            ClientName = "Test Client",
            Status = "Active",
            CreatedDate = DateTime.Now,
            CreatedBy = "TestUser"
        };

        _mockEngagementRepository
            .Setup(r => r.GetEngagementByCodeAsync(code))
            .ReturnsAsync(engagement);

        var query = new GetEngagementByCodeQuery(code);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(code, result.EngagementCode);
        Assert.AreEqual("Test Engagement", result.EngagementName);

        _mockEngagementRepository.Verify(r => r.GetEngagementByCodeAsync(code), Times.Once);
    }

    [Test]
    public async Task Handle_WithInvalidCode_ReturnsNull()
    {
        // Arrange
        var code = "INVALID";
        _mockEngagementRepository
            .Setup(r => r.GetEngagementByCodeAsync(code))
            .ReturnsAsync((Engagement?)null);

        var query = new GetEngagementByCodeQuery(code);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNull(result);

        _mockEngagementRepository.Verify(r => r.GetEngagementByCodeAsync(code), Times.Once);
    }
}

/// <summary>
/// Unit tests for GetEngagementWithFunds Query Handler.
/// </summary>
[TestFixture]
public class GetEngagementWithFundsQueryHandlerTests
{
    private Mock<IEngagementRepository> _mockEngagementRepository = null!;
    private GetEngagementWithFundsQueryHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _mockEngagementRepository = new Mock<IEngagementRepository>();
        _handler = new GetEngagementWithFundsQueryHandler(_mockEngagementRepository.Object);
    }

    [Test]
    public async Task Handle_WithValidEngagementId_ReturnsEngagementWithFunds()
    {
        // Arrange
        var engagementId = 1;
        var engagement = new Engagement
        {
            Id = engagementId,
            EngagementName = "Test Engagement",
            EngagementCode = "ENG001",
            ClientName = "Test Client",
            Status = "Active",
            CreatedDate = DateTime.Now,
            CreatedBy = "TestUser",
            Funds = new List<Fund>
            {
                new Fund
                {
                    Id = 1,
                    FundName = "Fund 1",
                    FundCode = "F001",
                    EngagementId = engagementId,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "TestUser"
                },
                new Fund
                {
                    Id = 2,
                    FundName = "Fund 2",
                    FundCode = "F002",
                    EngagementId = engagementId,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "TestUser"
                }
            }
        };

        _mockEngagementRepository
            .Setup(r => r.GetEngagementWithFundsAsync(engagementId))
            .ReturnsAsync(engagement);

        var query = new GetEngagementWithFundsQuery(engagementId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(engagementId, result.Id);
        Assert.AreEqual("Test Engagement", result.EngagementName);
        Assert.AreEqual(2, result.Funds.Count);
        Assert.AreEqual("Fund 1", result.Funds.First().FundName);

        _mockEngagementRepository.Verify(r => r.GetEngagementWithFundsAsync(engagementId), Times.Once);
    }

    [Test]
    public async Task Handle_WithEngagementButNoFunds_ReturnsEngagementWithEmptyFunds()
    {
        // Arrange
        var engagementId = 1;
        var engagement = new Engagement
        {
            Id = engagementId,
            EngagementName = "Test Engagement",
            EngagementCode = "ENG001",
            ClientName = "Test Client",
            Status = "Active",
            CreatedDate = DateTime.Now,
            CreatedBy = "TestUser",
            Funds = new List<Fund>()
        };

        _mockEngagementRepository
            .Setup(r => r.GetEngagementWithFundsAsync(engagementId))
            .ReturnsAsync(engagement);

        var query = new GetEngagementWithFundsQuery(engagementId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(engagementId, result.Id);
        Assert.AreEqual(0, result.Funds.Count);

        _mockEngagementRepository.Verify(r => r.GetEngagementWithFundsAsync(engagementId), Times.Once);
    }

    [Test]
    public async Task Handle_WithInvalidEngagementId_ReturnsNull()
    {
        // Arrange
        var engagementId = 999;
        _mockEngagementRepository
            .Setup(r => r.GetEngagementWithFundsAsync(engagementId))
            .ReturnsAsync((Engagement?)null);

        var query = new GetEngagementWithFundsQuery(engagementId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNull(result);

        _mockEngagementRepository.Verify(r => r.GetEngagementWithFundsAsync(engagementId), Times.Once);
    }
}
