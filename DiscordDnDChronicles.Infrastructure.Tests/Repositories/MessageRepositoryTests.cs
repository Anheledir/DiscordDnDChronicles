using Bogus;
using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Infrastructure.Data;
using DiscordDnDChronicles.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace DiscordDnDChronicles.Infrastructure.Tests.Repositories;

public class MessageRepositoryTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    private readonly Faker<Message> _messageFaker = new Faker<Message>()
        .RuleFor(m => m.DiscordMessageId, f => f.Random.AlphaNumeric(10))
        .RuleFor(m => m.Type, f => "Default")
        .RuleFor(m => m.Timestamp, f => f.Date.Past())
        .RuleFor(m => m.Content, f => f.Lorem.Paragraph())
        .RuleFor(m => m.AuthorDiscordId, f => f.Random.AlphaNumeric(10))
        .RuleFor(m => m.AuthorName, f => f.Person.UserName);

    private static async Task<Channel> CreateChannelAsync(AppDbContext context)
    {
        var channel = new Channel
        {
            DiscordChannelId = "testChannelId",
            Name = "Test Channel",
            Type = "GuildTextChat",
            CategoryId = "catId",
            Category = "TestCategory",
            Topic = "Test Topic"
        };
        context.Channels.Add(channel);
        await context.SaveChangesAsync();
        return channel;
    }

    [Fact]
    public async Task AddAsync_Should_Add_Message()
    {
        using var context = CreateContext();
        var repository = new MessageRepository(context);
        var channel = await CreateChannelAsync(context);
        var message = _messageFaker.Clone()
            .RuleFor(m => m.ChannelId, _ => channel.Id)
            .Generate();

        await repository.AddAsync(message);
        var retrieved = await repository.GetByIdAsync(message.Id);

        retrieved.ShouldNotBeNull();
        retrieved.DiscordMessageId.ShouldBe(message.DiscordMessageId);
        retrieved.ChannelId.ShouldBe(channel.Id);
    }

    [Fact]
    public async Task GetByDiscordMessageIdAsync_Should_Return_Message()
    {
        using var context = CreateContext();
        var repository = new MessageRepository(context);
        var channel = await CreateChannelAsync(context);
        var message = _messageFaker.Clone()
            .RuleFor(m => m.ChannelId, _ => channel.Id)
            .Generate();

        await repository.AddAsync(message);
        var retrieved = await repository.GetByDiscordMessageIdAsync(message.DiscordMessageId);

        retrieved.ShouldNotBeNull();
        retrieved.Id.ShouldBe(message.Id);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Messages()
    {
        using var context = CreateContext();
        var repository = new MessageRepository(context);
        var channel = await CreateChannelAsync(context);
        var messages = _messageFaker.Clone()
            .RuleFor(m => m.ChannelId, _ => channel.Id)
            .Generate(5);

        foreach (var msg in messages)
        {
            await repository.AddAsync(msg);
        }

        var retrieved = await repository.GetAllAsync();
        retrieved.Count().ShouldBe(5);
    }

    [Fact]
    public async Task GetByChannelIdAsync_Should_Return_Messages_For_Channel()
    {
        using var context = CreateContext();
        var repository = new MessageRepository(context);
        var channel = await CreateChannelAsync(context);
        var messages = _messageFaker.Clone()
            .RuleFor(m => m.ChannelId, _ => channel.Id)
            .Generate(3);

        foreach (var msg in messages)
        {
            await repository.AddAsync(msg);
        }

        var retrieved = await repository.GetByChannelIdAsync(channel.Id);
        retrieved.Count().ShouldBe(3);
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Message()
    {
        using var context = CreateContext();
        var repository = new MessageRepository(context);
        var channel = await CreateChannelAsync(context);
        var message = _messageFaker.Clone()
            .RuleFor(m => m.ChannelId, _ => channel.Id)
            .Generate();
        await repository.AddAsync(message);

        message.Content = "Updated Content";
        await repository.UpdateAsync(message);
        var retrieved = await repository.GetByIdAsync(message.Id);
        retrieved.Content.ShouldBe("Updated Content");
    }

    [Fact]
    public async Task DeleteAsync_Should_Remove_Message()
    {
        using var context = CreateContext();
        var repository = new MessageRepository(context);
        var channel = await CreateChannelAsync(context);
        var message = _messageFaker.Clone()
            .RuleFor(m => m.ChannelId, _ => channel.Id)
            .Generate();
        await repository.AddAsync(message);

        await repository.DeleteAsync(message);
        var retrieved = await repository.GetByIdAsync(message.Id);
        retrieved.ShouldBeNull();
    }
}
