using Bogus;
using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Infrastructure.Data;
using DiscordDnDChronicles.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace DiscordDnDChronicles.Infrastructure.Tests.Repositories;

public class MapRepositoryTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

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

    private static async Task<Location> CreateLocationAsync(AppDbContext context, int campaignId)
    {
        var location = new Location
        {
            Name = "Test Location",
            Description = "Test Location Description",
            Slug = "test-location",
            CampaignId = campaignId
        };
        context.Locations.Add(location);
        await context.SaveChangesAsync();
        return location;
    }

    private readonly Faker<Map> _mapFaker = new Faker<Map>()
        .RuleFor(m => m.Title, f => f.Lorem.Word())
        .RuleFor(m => m.Description, f => f.Lorem.Sentence())
        .RuleFor(m => m.Url, f => f.Internet.Url());

    [Fact]
    public async Task AddAsync_Should_Add_Map()
    {
        using var context = CreateContext();
        var repository = new MapRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var location = await CreateLocationAsync(context, campaign.Id);
        var map = _mapFaker.Clone()
            .RuleFor(m => m.LocationId, _ => location.Id)
            .Generate();

        await repository.AddAsync(map);
        var retrieved = await repository.GetByIdAsync(map.Id);

        retrieved.ShouldNotBeNull();
        retrieved.Title.ShouldBe(map.Title);
        retrieved.LocationId.ShouldBe(location.Id);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Maps()
    {
        using var context = CreateContext();
        var repository = new MapRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var location = await CreateLocationAsync(context, campaign.Id);
        var maps = _mapFaker.Clone()
            .RuleFor(m => m.LocationId, _ => location.Id)
            .Generate(5);

        foreach (var map in maps)
        {
            await repository.AddAsync(map);
        }

        var retrieved = await repository.GetAllAsync();
        retrieved.Count().ShouldBe(5);
    }

    [Fact]
    public async Task GetByLocationIdAsync_Should_Return_Maps_For_Location()
    {
        using var context = CreateContext();
        var repository = new MapRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var location = await CreateLocationAsync(context, campaign.Id);
        var maps = _mapFaker.Clone()
            .RuleFor(m => m.LocationId, _ => location.Id)
            .Generate(3);

        foreach (var map in maps)
        {
            await repository.AddAsync(map);
        }

        var retrieved = await repository.GetByLocationIdAsync(location.Id);
        retrieved.Count().ShouldBe(3);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Map()
    {
        using var context = CreateContext();
        var repository = new MapRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var location = await CreateLocationAsync(context, campaign.Id);
        var map = _mapFaker.Clone()
            .RuleFor(m => m.LocationId, _ => location.Id)
            .Generate();
        await repository.AddAsync(map);

        map.Title = "Updated Map Title";
        await repository.UpdateAsync(map);
        var retrieved = await repository.GetByIdAsync(map.Id);
        retrieved.Title.ShouldBe("Updated Map Title");
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_Map()
    {
        using var context = CreateContext();
        var repository = new MapRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var location = await CreateLocationAsync(context, campaign.Id);
        var map = _mapFaker.Clone()
            .RuleFor(m => m.LocationId, _ => location.Id)
            .Generate();
        await repository.AddAsync(map);

        await repository.DeleteAsync(map);
        var retrieved = await repository.GetByIdAsync(map.Id);
        retrieved.ShouldBeNull();
    }
}
