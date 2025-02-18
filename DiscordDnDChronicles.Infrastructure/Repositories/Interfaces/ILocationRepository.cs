using DiscordDnDChronicles.Core.Domain.Models;

namespace DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;

public interface ILocationRepository
{
    /// <summary>
    /// Retrieves a location by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the location.</param>
    /// <returns>The Location entity if found, otherwise null.</returns>
    Task<Location> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all locations.
    /// </summary>
    /// <returns>An enumerable collection of Location entities.</returns>
    Task<IEnumerable<Location>> GetAllAsync();

    /// <summary>
    /// Retrieves all locations associated with a specific campaign.
    /// </summary>
    /// <param name="campaignId">The unique identifier of the campaign.</param>
    /// <returns>An enumerable collection of Location entities belonging to the campaign.</returns>
    Task<IEnumerable<Location>> GetByCampaignIdAsync(int campaignId);

    /// <summary>
    /// Retrieves a location by its slug.
    /// </summary>
    /// <param name="slug">The slug for the location.</param>
    /// <returns>The Location entity if found, otherwise null.</returns>
    Task<Location> GetBySlugAsync(string slug);

    /// <summary>
    /// Adds a new location.
    /// </summary>
    /// <param name="location">The Location entity to add.</param>
    Task AddAsync(Location location);

    /// <summary>
    /// Updates an existing location.
    /// </summary>
    /// <param name="location">The Location entity with updated values.</param>
    Task UpdateAsync(Location location);

    /// <summary>
    /// Deletes a location.
    /// </summary>
    /// <param name="location">The Location entity to delete.</param>
    Task DeleteAsync(Location location);
}
