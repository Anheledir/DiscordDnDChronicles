using Bogus;
using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Infrastructure.Data;
using DiscordDnDChronicles.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace DiscordDnDChronicles.Infrastructure.Tests.Repositories;

public class CharacterGroupRepositoryTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    private readonly Faker<CharacterGroup> _groupFaker = new Faker<CharacterGroup>()
        .RuleFor(g => g.Name, f => f.Lorem.Word())
        .RuleFor(g => g.Description, f => f.Lorem.Sentence());

    [Fact]
    public async Task AddAsync_Should_Add_CharacterGroup()
    {
        using var context = CreateContext();
        var repository = new CharacterGroupRepository(context);
        var group = _groupFaker.Generate();

        await repository.AddAsync(group);
        var retrieved = await repository.GetByIdAsync(group.Id);

        retrieved.ShouldNotBeNull();
        retrieved.Name.ShouldBe(group.Name);
        retrieved.Description.ShouldBe(group.Description);
    }

    [Fact]
    public async Task GetByNameAsync_Should_Return_CharacterGroup()
    {
        using var context = CreateContext();
        var repository = new CharacterGroupRepository(context);
        var group = _groupFaker.Generate();

        await repository.AddAsync(group);
        var retrieved = await repository.GetByNameAsync(group.Name);

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(group.Id);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_CharacterGroups()
    {
        using var context = CreateContext();
        var repository = new CharacterGroupRepository(context);
        var groups = _groupFaker.Generate(5);
        foreach (var group in groups)
        {
            await repository.AddAsync(group);
        }

        var retrieved = await repository.GetAllAsync();
        retrieved.Count().ShouldBe(5);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_CharacterGroup()
    {
        using var context = CreateContext();
        var repository = new CharacterGroupRepository(context);
        var group = _groupFaker.Generate();

        await repository.AddAsync(group);
        group.Name = "Updated Group Name";
        await repository.UpdateAsync(group);
        var retrieved = await repository.GetByIdAsync(group.Id);

        retrieved.Name.ShouldBe("Updated Group Name");
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_CharacterGroup()
    {
        using var context = CreateContext();
        var repository = new CharacterGroupRepository(context);
        var group = _groupFaker.Generate();

        await repository.AddAsync(group);
        await repository.DeleteAsync(group);
        var retrieved = await repository.GetByIdAsync(group.Id);

        retrieved.ShouldBeNull();
    }
}
