using Bogus;
using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Infrastructure.Data;
using DiscordDnDChronicles.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace DiscordDnDChronicles.Infrastructure.Tests.Repositories;

public class LocationRepositoryTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    private readonly Faker<Location> _locationFaker = new Faker<Location>()
        .RuleFor(l => l.Name, f => f.Address.City())
        .RuleFor(l => l.Description, f => f.Lorem.Sentence())
        .RuleFor(l => l.Slug, f => f.Lorem.Word());

    private static async Task<Campaign> CreateCampaignAsync(AppDbContext context)
    {
        var campaign = new Campaign
        {
            Name = "Test Campaign",
            Description = "Test Campaign Description",
            StartDate = DateTime.UtcNow
        };
        context.Campaigns.Add(campaign);
        await context.SaveChangesAsync();
        return campaign;
    }

    [Fact]
    public async Task AddAsync_Should_Add_Location()
    {
        using var context = CreateContext();
        var repository = new LocationRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var location = _locationFaker.Clone()
            .RuleFor(l => l.CampaignId, _ => campaign.Id)
            .Generate();

        await repository.AddAsync(location);
        var retrieved = await repository.GetByIdAsync(location.Id);

        retrieved.ShouldNotBeNull();
        retrieved.Name.ShouldBe(location.Name);
        retrieved.Description.ShouldBe(location.Description);
        retrieved.CampaignId.ShouldBe(campaign.Id);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Locations()
    {
        using var context = CreateContext();
        var repository = new LocationRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var locations = _locationFaker.Clone()
            .RuleFor(l => l.CampaignId, _ => campaign.Id)
            .Generate(5);

        foreach (var loc in locations)
        {
            await repository.AddAsync(loc);
        }

        var retrieved = await repository.GetAllAsync();
        retrieved.Count().ShouldBe(5);
    }

    [Fact]
    public async Task GetByCampaignIdAsync_Should_Return_Locations_For_Campaign()
    {
        using var context = CreateContext();
        var repository = new LocationRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var locations = _locationFaker.Clone()
            .RuleFor(l => l.CampaignId, _ => campaign.Id)
            .Generate(3);

        foreach (var loc in locations)
        {
            await repository.AddAsync(loc);
        }

        var retrieved = await repository.GetByCampaignIdAsync(campaign.Id);
        retrieved.Count().ShouldBe(3);
    }

    [Fact]
    public async Task GetBySlugAsync_Should_Return_Location()
    {
        using var context = CreateContext();
        var repository = new LocationRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var location = _locationFaker.Clone()
            .RuleFor(l => l.CampaignId, _ => campaign.Id)
            .Generate();
        await repository.AddAsync(location);

        var retrieved = await repository.GetBySlugAsync(location.Slug);
        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(location.Id);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Location()
    {
        using var context = CreateContext();
        var repository = new LocationRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var location = _locationFaker.Clone()
            .RuleFor(l => l.CampaignId, _ => campaign.Id)
            .Generate();
        await repository.AddAsync(location);

        location.Name = "Updated Location Name";
        await repository.UpdateAsync(location);
        var retrieved = await repository.GetByIdAsync(location.Id);
        retrieved.Name.ShouldBe("Updated Location Name");
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_Location()
    {
        using var context = CreateContext();
        var repository = new LocationRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var location = _locationFaker.Clone()
            .RuleFor(l => l.CampaignId, _ => campaign.Id)
            .Generate();
        await repository.AddAsync(location);

        await repository.DeleteAsync(location);
        var retrieved = await repository.GetByIdAsync(location.Id);
        retrieved.ShouldBeNull();
    }
}
