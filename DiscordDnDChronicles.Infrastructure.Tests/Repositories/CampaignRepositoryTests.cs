using Bogus;
using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Infrastructure.Data;
using DiscordDnDChronicles.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace DiscordDnDChronicles.Infrastructure.Tests.Repositories;

public class CampaignRepositoryTests
{
    private readonly Faker<Campaign> _campaignFaker;

    public CampaignRepositoryTests()
    {
        _campaignFaker = new Faker<Campaign>()
            .RuleFor(c => c.Name, f => f.Lorem.Word())
            .RuleFor(c => c.Description, f => f.Lorem.Sentence())
            .RuleFor(c => c.StartDate, f => f.Date.Past());
    }

    // Helper method to create a new AppDbContext with a unique in-memory database for each test.
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAsync_Should_Add_Campaign()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new CampaignRepository(context);
        var campaign = _campaignFaker.Generate();

        // Act
        await repository.AddAsync(campaign);
        var retrieved = await repository.GetByIdAsync(campaign.Id);

        // Assert
        retrieved.ShouldNotBeNull();
        retrieved.Name.ShouldBe(campaign.Name);
        retrieved.Description.ShouldBe(campaign.Description);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Campaigns()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new CampaignRepository(context);
        var campaigns = _campaignFaker.Generate(5);
        foreach (var campaign in campaigns)
        {
            await repository.AddAsync(campaign);
        }

        // Act
        var retrieved = await repository.GetAllAsync();

        // Assert
        retrieved.Count().ShouldBe(5);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Campaign()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new CampaignRepository(context);
        var campaign = _campaignFaker.Generate();
        await repository.AddAsync(campaign);

        // Act
        campaign.Name = "UpdatedCampaignName";
        await repository.UpdateAsync(campaign);
        var retrieved = await repository.GetByIdAsync(campaign.Id);

        // Assert
        retrieved.Name.ShouldBe("UpdatedCampaignName");
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_Campaign()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new CampaignRepository(context);
        var campaign = _campaignFaker.Generate();
        await repository.AddAsync(campaign);

        // Act
        await repository.DeleteAsync(campaign);
        var retrieved = await repository.GetByIdAsync(campaign.Id);

        // Assert
        retrieved.ShouldBeNull();
    }
}
