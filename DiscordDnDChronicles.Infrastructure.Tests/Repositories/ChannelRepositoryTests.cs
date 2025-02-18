using Bogus;
using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Infrastructure.Data;
using DiscordDnDChronicles.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace DiscordDnDChronicles.Infrastructure.Tests.Repositories;

public class ChannelRepositoryTests
{
    private readonly Faker<Channel> _channelFaker;

    public ChannelRepositoryTests()
    {
        _channelFaker = new Faker<Channel>()
            .RuleFor(c => c.DiscordChannelId, f => f.Random.AlphaNumeric(10))
            .RuleFor(c => c.Name, f => f.Commerce.ProductName())
            .RuleFor(c => c.Type, f => "GuildTextChat")
            .RuleFor(c => c.CategoryId, f => f.Random.AlphaNumeric(10))
            .RuleFor(c => c.Category, f => f.Lorem.Word())
            .RuleFor(c => c.Topic, f => f.Lorem.Sentence());
    }

    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAsync_Should_Add_Channel()
    {
        using var context = CreateContext();
        var repository = new ChannelRepository(context);
        var channel = _channelFaker.Generate();

        await repository.AddAsync(channel);
        var retrieved = await repository.GetByIdAsync(channel.Id);

        retrieved.ShouldNotBeNull();
        retrieved.DiscordChannelId.ShouldBe(channel.DiscordChannelId);
        retrieved.Name.ShouldBe(channel.Name);
    }

    [Fact]
    public async Task GetByDiscordChannelIdAsync_Should_Return_Channel()
    {
        using var context = CreateContext();
        var repository = new ChannelRepository(context);
        var channel = _channelFaker.Generate();
        await repository.AddAsync(channel);

        var retrieved = await repository.GetByDiscordChannelIdAsync(channel.DiscordChannelId);

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(channel.Id);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Channels()
    {
        using var context = CreateContext();
        var repository = new ChannelRepository(context);
        var channels = _channelFaker.Generate(5);
        foreach (var ch in channels)
        {
            await repository.AddAsync(ch);
        }

        var retrieved = await repository.GetAllAsync();

        retrieved.Count().ShouldBe(5);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Channel()
    {
        using var context = CreateContext();
        var repository = new ChannelRepository(context);
        var channel = _channelFaker.Generate();
        await repository.AddAsync(channel);

        channel.Name = "Updated Channel Name";
        await repository.UpdateAsync(channel);
        var retrieved = await repository.GetByIdAsync(channel.Id);

        retrieved.Name.ShouldBe("Updated Channel Name");
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_Channel()
    {
        using var context = CreateContext();
        var repository = new ChannelRepository(context);
        var channel = _channelFaker.Generate();
        await repository.AddAsync(channel);

        await repository.DeleteAsync(channel);
        var retrieved = await repository.GetByIdAsync(channel.Id);

        retrieved.ShouldBeNull();
    }
}
