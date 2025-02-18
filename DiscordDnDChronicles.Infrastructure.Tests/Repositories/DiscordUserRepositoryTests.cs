using Bogus;
using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Infrastructure.Data;
using DiscordDnDChronicles.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace DiscordDnDChronicles.Infrastructure.Tests.Repositories;

public class DiscordUserRepositoryTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    private readonly Faker<DiscordUser> _userFaker = new Faker<DiscordUser>()
        .RuleFor(u => u.DiscordUserId, f => f.Random.AlphaNumeric(10))
        .RuleFor(u => u.Nickname, f => f.Internet.UserName());

    [Fact]
    public async Task AddAsync_Should_Add_DiscordUser()
    {
        using var context = CreateContext();
        var repository = new DiscordUserRepository(context);
        var discordUser = _userFaker.Generate();

        await repository.AddAsync(discordUser);
        var retrieved = await repository.GetByIdAsync(discordUser.Id);

        retrieved.ShouldNotBeNull();
        retrieved.DiscordUserId.ShouldBe(discordUser.DiscordUserId);
        retrieved.Nickname.ShouldBe(discordUser.Nickname);
    }

    [Fact]
    public async Task GetByDiscordUserIdAsync_Should_Return_DiscordUser()
    {
        using var context = CreateContext();
        var repository = new DiscordUserRepository(context);
        var discordUser = _userFaker.Generate();

        await repository.AddAsync(discordUser);
        var retrieved = await repository.GetByDiscordUserIdAsync(discordUser.DiscordUserId);

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(discordUser.Id);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_DiscordUsers()
    {
        using var context = CreateContext();
        var repository = new DiscordUserRepository(context);
        var users = _userFaker.Generate(5);
        foreach (var user in users)
        {
            await repository.AddAsync(user);
        }

        var retrieved = await repository.GetAllAsync();
        retrieved.Count().ShouldBe(5);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_DiscordUser()
    {
        using var context = CreateContext();
        var repository = new DiscordUserRepository(context);
        var discordUser = _userFaker.Generate();
        await repository.AddAsync(discordUser);

        discordUser.Nickname = "UpdatedNickname";
        await repository.UpdateAsync(discordUser);
        var retrieved = await repository.GetByIdAsync(discordUser.Id);
        retrieved.Nickname.ShouldBe("UpdatedNickname");
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_DiscordUser()
    {
        using var context = CreateContext();
        var repository = new DiscordUserRepository(context);
        var discordUser = _userFaker.Generate();
        await repository.AddAsync(discordUser);

        await repository.DeleteAsync(discordUser);
        var retrieved = await repository.GetByIdAsync(discordUser.Id);
        retrieved.ShouldBeNull();
    }
}
