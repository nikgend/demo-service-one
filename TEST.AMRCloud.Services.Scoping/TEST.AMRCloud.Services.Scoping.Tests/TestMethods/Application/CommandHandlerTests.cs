using NUnit.Framework;
using Moq;
using TEST.AMRCloud.Services.Scoping.Application.DbOps;
using TEST.AMRCloud.Services.Scoping.Domain.AggregateRoots;
using TEST.AMRCloud.Services.Scoping.Infrastructure.Repositories;

namespace TEST.AMRCloud.Services.Scoping.Tests.TestMethods.Application;

/// <summary>
/// Unit tests for CreateEngagementCommand Handler.
/// </summary>
[TestFixture]
public class CreateEngagementCommandHandlerTests
{
    private Mock<IEngagementRepository> _mockEngagementRepository = null!;
    private CreateEngagementCommandHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _mockEngagementRepository = new Mock<IEngagementRepository>();
        _handler = new CreateEngagementCommandHandler(_mockEngagementRepository.Object);
    }

    [Test]
    public async Task Handle_WithValidData_CreatesEngagementAndReturnsDto()
    {
        // Arrange
        var engagementName = "New Engagement";
        var engagementCode = "ENG999";
        var engagementPartner = "New Partner";
        var engagementManager = "John Manager";
        var periodEndDate = DateTime.Now.AddMonths(6);
        var createdBy = "TestUser";

        var createdEngagement = new Engagement
        {
            Id = 1,
            EngagementName = engagementName,
            EngagementCode = engagementCode,
            EngagementPartner = engagementPartner,
            EngagementManager = engagementManager,
            PeriodEndDate = periodEndDate,
            CreatedDate = DateTime.Now,
            CreatedBy = createdBy,
            IsActive = true
        };

        _mockEngagementRepository
            .Setup(r => r.AddAsync(It.IsAny<Engagement>()))
            .ReturnsAsync(createdEngagement);

        var command = new CreateEngagementCommand(engagementName, engagementCode,
            engagementManager, engagementPartner, periodEndDate, createdBy);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(engagementName, result.EngagementName);
        Assert.AreEqual(engagementCode, result.EngagementCode);
        Assert.AreEqual(engagementPartner, result.EngagementPartner);
        Assert.AreEqual(engagementManager, result.EngagementManager);
        Assert.AreEqual(createdBy, result.CreatedBy);

        _mockEngagementRepository.Verify(r => r.AddAsync(It.IsAny<Engagement>()), Times.Once);
    }

    [Test]
    public async Task Handle_WhenRepositoryReturnsNull_ReturnsNull()
    {
        // Arrange
        _mockEngagementRepository
            .Setup(r => r.AddAsync(It.IsAny<Engagement>()))
            .ReturnsAsync((Engagement?)null);

        var command = new CreateEngagementCommand("Engagement", "ENG001",
            "Test Manager", "Test Partner", DateTime.Now, "TestUser");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNull(result);
        _mockEngagementRepository.Verify(r => r.AddAsync(It.IsAny<Engagement>()), Times.Once);
    }
}

/// <summary>
/// Unit tests for UpdateEngagementCommand Handler.
/// </summary>
[TestFixture]
public class UpdateEngagementCommandHandlerTests
{
    private Mock<IEngagementRepository> _mockEngagementRepository = null!;
    private UpdateEngagementCommandHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _mockEngagementRepository = new Mock<IEngagementRepository>();
        _handler = new UpdateEngagementCommandHandler(_mockEngagementRepository.Object);
    }

    [Test]
    public async Task Handle_WithValidData_UpdatesEngagementAndReturnsDto()
    {
        // Arrange
        var engagementId = 1;
        var existingEngagement = new Engagement
        {
            Id = engagementId,
            EngagementName = "Old Engagement",
            EngagementCode = "ENG001",
            EngagementPartner = "Old Partner",
            EngagementManager = "Old Manager",
            PeriodEndDate = DateTime.Now,
            CreatedDate = DateTime.Now.AddMonths(-6),
            CreatedBy = "TestUser"
        };

        var updatedEngagement = new Engagement
        {
            Id = engagementId,
            EngagementName = "Updated Engagement",
            EngagementCode = "ENG001",
            EngagementPartner = "Updated Partner",
            EngagementManager = "Updated Manager",
            PeriodEndDate = DateTime.Now.AddMonths(6)
        };

        _mockEngagementRepository
            .Setup(r => r.GetByIdAsync(engagementId))
            .ReturnsAsync(existingEngagement);

        _mockEngagementRepository
            .Setup(r => r.UpdateAsync(It.IsAny<Engagement>()))
            .ReturnsAsync(updatedEngagement);

        var command = new UpdateEngagementCommand(engagementId, "Updated Engagement", "ENG001",
            "Updated Manager", "Updated Partner", DateTime.Now.AddMonths(6), "UpdateUser");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Updated Engagement", result.EngagementName);
        Assert.AreEqual("Updated Partner", result.EngagementPartner);
        Assert.AreEqual("Updated Manager", result.EngagementManager);

        _mockEngagementRepository.Verify(r => r.GetByIdAsync(engagementId), Times.Once);
        _mockEngagementRepository.Verify(r => r.UpdateAsync(It.IsAny<Engagement>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithNonExistentEngagement_ReturnsNull()
    {
        // Arrange
        var engagementId = 999;

        _mockEngagementRepository
            .Setup(r => r.GetByIdAsync(engagementId))
            .ReturnsAsync((Engagement?)null);

        var command = new UpdateEngagementCommand(engagementId, "Engagement", "ENG001",
            "Manager", "Partner", DateTime.Now, "UpdateUser");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNull(result);
        _mockEngagementRepository.Verify(r => r.GetByIdAsync(engagementId), Times.Once);
        _mockEngagementRepository.Verify(r => r.UpdateAsync(It.IsAny<Engagement>()), Times.Never);
    }

    [Test]
    public async Task Handle_PartialUpdate_PreservesUnchangedFields()
    {
        // Arrange
        var engagementId = 1;
        var existingEngagement = new Engagement
        {
            Id = engagementId,
            EngagementName = "Engagement",
            EngagementCode = "ENG001",
            EngagementPartner = "Partner",
            EngagementManager = "Active Manager",
            PeriodEndDate = DateTime.Now.AddMonths(6),
            CreatedDate = DateTime.Now,
            CreatedBy = "TestUser"
        };

        var updatedEngagement = new Engagement
        {
            Id = engagementId,
            EngagementName = "Engagement",
            EngagementCode = "ENG001",
            EngagementPartner = "Updated Partner",
            EngagementManager = "Active Manager",
            PeriodEndDate = DateTime.Now.AddMonths(6)
        };

        _mockEngagementRepository
            .Setup(r => r.GetByIdAsync(engagementId))
            .ReturnsAsync(existingEngagement);

        _mockEngagementRepository
            .Setup(r => r.UpdateAsync(It.IsAny<Engagement>()))
            .ReturnsAsync(updatedEngagement);

        // Only updating EngagementPartner, others should remain unchanged
        var command = new UpdateEngagementCommand(engagementId, null, null,
            null, "Updated Partner", null, "UpdateUser");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Updated Partner", result.EngagementPartner);
        Assert.AreEqual("Engagement", result.EngagementName);
        Assert.AreEqual("ENG001", result.EngagementCode);

        _mockEngagementRepository.Verify(r => r.GetByIdAsync(engagementId), Times.Once);
        _mockEngagementRepository.Verify(r => r.UpdateAsync(It.IsAny<Engagement>()), Times.Once);
    }
}

/// <summary>
/// Unit tests for DeleteEngagementCommand Handler.
/// </summary>
[TestFixture]
public class DeleteEngagementCommandHandlerTests
{
    private Mock<IEngagementRepository> _mockEngagementRepository = null!;
    private DeleteEngagementCommandHandler _handler = null!;

    [SetUp]
    public void SetUp()
    {
        _mockEngagementRepository = new Mock<IEngagementRepository>();
        _handler = new DeleteEngagementCommandHandler(_mockEngagementRepository.Object);
    }

    [Test]
    public async Task Handle_WithExistingEngagement_DeletesAndReturnsTrue()
    {
        // Arrange
        var engagementId = 1;
        var engagement = new Engagement
        {
            Id = engagementId,
            EngagementName = "Test Engagement",
            EngagementCode = "ENG001"
        };

        _mockEngagementRepository
            .Setup(r => r.GetByIdAsync(engagementId))
            .ReturnsAsync(engagement);

        _mockEngagementRepository
            .Setup(r => r.DeleteAsync(It.IsAny<Engagement>()))
            .ReturnsAsync(true);

        var command = new DeleteEngagementCommand(engagementId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result);
        _mockEngagementRepository.Verify(r => r.GetByIdAsync(engagementId), Times.Once);
        _mockEngagementRepository.Verify(r => r.DeleteAsync(It.IsAny<Engagement>()), Times.Once);
    }

    [Test]
    public async Task Handle_WithNonExistentEngagement_ReturnsFalse()
    {
        // Arrange
        var engagementId = 999;

        _mockEngagementRepository
            .Setup(r => r.GetByIdAsync(engagementId))
            .ReturnsAsync((Engagement?)null);

        var command = new DeleteEngagementCommand(engagementId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsFalse(result);
        _mockEngagementRepository.Verify(r => r.GetByIdAsync(engagementId), Times.Once);
        _mockEngagementRepository.Verify(r => r.DeleteAsync(It.IsAny<Engagement>()), Times.Never);
    }

    [Test]
    public async Task Handle_WhenDeleteFails_ReturnsFalse()
    {
        // Arrange
        var engagementId = 1;
        var engagement = new Engagement
        {
            Id = engagementId,
            EngagementName = "Test Engagement",
            EngagementCode = "ENG001"
        };

        _mockEngagementRepository
            .Setup(r => r.GetByIdAsync(engagementId))
            .ReturnsAsync(engagement);

        _mockEngagementRepository
            .Setup(r => r.DeleteAsync(It.IsAny<Engagement>()))
            .ReturnsAsync(false);

        var command = new DeleteEngagementCommand(engagementId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsFalse(result);
        _mockEngagementRepository.Verify(r => r.GetByIdAsync(engagementId), Times.Once);
        _mockEngagementRepository.Verify(r => r.DeleteAsync(It.IsAny<Engagement>()), Times.Once);
    }
}
