using Bogus;
using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Infrastructure.Data;
using DiscordDnDChronicles.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace DiscordDnDChronicles.Infrastructure.Tests.Repositories;

public class CharacterRepositoryTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    private readonly Faker<Character> _characterFaker = new Faker<Character>()
        .RuleFor(c => c.Name, f => f.Person.FullName)
        .RuleFor(c => c.Type, f => f.PickRandom<CharacterType>())
        .RuleFor(c => c.Description, f => f.Lorem.Sentence());

    private static async Task<Campaign> CreateCampaignAsync(AppDbContext context)
    {
        var campaign = new Campaign
        {
            Name = "Test Campaign",
            Description = "Test Campaign Description",
            StartDate = System.DateTime.UtcNow
        };
        context.Campaigns.Add(campaign);
        await context.SaveChangesAsync();
        return campaign;
    }

    [Fact]
    public async Task AddAsync_Should_Add_Character()
    {
        using var context = CreateContext();
        var repository = new CharacterRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var character = _characterFaker.Clone()
            .RuleFor(c => c.CampaignId, _ => campaign.Id)
            .Generate();

        await repository.AddAsync(character);
        var retrieved = await repository.GetByIdAsync(character.Id);

        retrieved.ShouldNotBeNull();
        retrieved.Name.ShouldBe(character.Name);
        retrieved.CampaignId.ShouldBe(campaign.Id);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Characters()
    {
        using var context = CreateContext();
        var repository = new CharacterRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var characters = _characterFaker.Clone()
            .RuleFor(c => c.CampaignId, _ => campaign.Id)
            .Generate(5);

        foreach (var character in characters)
        {
            await repository.AddAsync(character);
        }

        var retrieved = await repository.GetAllAsync();
        retrieved.Count().ShouldBe(5);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Character()
    {
        using var context = CreateContext();
        var repository = new CharacterRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var character = _characterFaker.Clone()
            .RuleFor(c => c.CampaignId, _ => campaign.Id)
            .Generate();
        await repository.AddAsync(character);

        var retrieved = await repository.GetByIdAsync(character.Id);
        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(character.Id);
    }

    [Fact]
    public async Task GetByCampaignIdAsync_Should_Return_Characters_For_Campaign()
    {
        using var context = CreateContext();
        var repository = new CharacterRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var characters = _characterFaker.Clone()
            .RuleFor(c => c.CampaignId, _ => campaign.Id)
            .Generate(3);

        foreach (var character in characters)
        {
            await repository.AddAsync(character);
        }

        var retrieved = await repository.GetByCampaignIdAsync(campaign.Id);
        retrieved.Count().ShouldBe(3);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Character()
    {
        using var context = CreateContext();
        var repository = new CharacterRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var character = _characterFaker.Clone()
            .RuleFor(c => c.CampaignId, _ => campaign.Id)
            .Generate();
        await repository.AddAsync(character);

        character.Name = "Updated Character Name";
        await repository.UpdateAsync(character);
        var retrieved = await repository.GetByIdAsync(character.Id);
        retrieved.Name.ShouldBe("Updated Character Name");
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_Character()
    {
        using var context = CreateContext();
        var repository = new CharacterRepository(context);
        var campaign = await CreateCampaignAsync(context);
        var character = _characterFaker.Clone()
            .RuleFor(c => c.CampaignId, _ => campaign.Id)
            .Generate();
        await repository.AddAsync(character);
        await repository.DeleteAsync(character);
        var retrieved = await repository.GetByIdAsync(character.Id);
        retrieved.ShouldBeNull();
    }
}
