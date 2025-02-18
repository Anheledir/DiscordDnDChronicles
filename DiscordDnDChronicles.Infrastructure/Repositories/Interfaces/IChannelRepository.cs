using DiscordDnDChronicles.Core.Domain.Models;

namespace DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;

public interface IChannelRepository
{
    /// <summary>
    /// Retrieves a channel by its internal database ID.
    /// </summary>
    /// <param name="id">The internal channel ID.</param>
    /// <returns>The channel entity if found, otherwise null.</returns>
    Task<Channel> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves a channel by its Discord unique channel ID.
    /// </summary>
    /// <param name="discordChannelId">The Discord channel ID as a string.</param>
    /// <returns>The channel entity if found, otherwise null.</returns>
    Task<Channel> GetByDiscordChannelIdAsync(string discordChannelId);

    /// <summary>
    /// Retrieves all channels.
    /// </summary>
    /// <returns>An enumerable collection of channels.</returns>
    Task<IEnumerable<Channel>> GetAllAsync();

    /// <summary>
    /// Adds a new channel.
    /// </summary>
    /// <param name="channel">The channel entity to add.</param>
    Task AddAsync(Channel channel);

    /// <summary>
    /// Updates an existing channel.
    /// </summary>
    /// <param name="channel">The channel entity with updated values.</param>
    Task UpdateAsync(Channel channel);

    /// <summary>
    /// Deletes a channel.
    /// </summary>
    /// <param name="channel">The channel entity to delete.</param>
    Task DeleteAsync(Channel channel);
}
