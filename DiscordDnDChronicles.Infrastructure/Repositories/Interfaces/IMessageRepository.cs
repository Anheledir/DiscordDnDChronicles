using DiscordDnDChronicles.Core.Domain.Models;

namespace DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;

public interface IMessageRepository
{
    /// <summary>
    /// Retrieves a message by its internal database identifier.
    /// </summary>
    /// <param name="id">The internal ID of the message.</param>
    /// <returns>The Message entity if found, otherwise null.</returns>
    Task<Message> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves a message by its unique Discord message ID.
    /// </summary>
    /// <param name="discordMessageId">The unique Discord message ID.</param>
    /// <returns>The Message entity if found, otherwise null.</returns>
    Task<Message> GetByDiscordMessageIdAsync(string discordMessageId);

    /// <summary>
    /// Retrieves all messages.
    /// </summary>
    /// <returns>An enumerable collection of Message entities.</returns>
    Task<IEnumerable<Message>> GetAllAsync();

    /// <summary>
    /// Retrieves all messages associated with a specific channel.
    /// </summary>
    /// <param name="channelId">The internal channel identifier.</param>
    /// <returns>An enumerable collection of Message entities for the given channel.</returns>
    Task<IEnumerable<Message>> GetByChannelIdAsync(int channelId);

    /// <summary>
    /// Adds a new message.
    /// </summary>
    /// <param name="message">The Message entity to add.</param>
    Task AddAsync(Message message);

    /// <summary>
    /// Updates an existing message.
    /// </summary>
    /// <param name="message">The Message entity with updated values.</param>
    Task UpdateAsync(Message message);

    /// <summary>
    /// Deletes a message.
    /// </summary>
    /// <param name="message">The Message entity to delete.</param>
    Task DeleteAsync(Message message);
}
