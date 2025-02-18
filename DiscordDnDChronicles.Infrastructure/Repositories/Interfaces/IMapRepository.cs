using DiscordDnDChronicles.Core.Domain.Models;

namespace DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;

public interface IMapRepository
{
    /// <summary>
    /// Retrieves a map by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the map.</param>
    /// <returns>The Map entity if found, otherwise null.</returns>
    Task<Map> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all maps.
    /// </summary>
    /// <returns>An enumerable collection of Map entities.</returns>
    Task<IEnumerable<Map>> GetAllAsync();

    /// <summary>
    /// Retrieves all maps associated with a specific location.
    /// </summary>
    /// <param name="locationId">The unique identifier of the location.</param>
    /// <returns>An enumerable collection of Map entities for the location.</returns>
    Task<IEnumerable<Map>> GetByLocationIdAsync(int locationId);

    /// <summary>
    /// Adds a new map.
    /// </summary>
    /// <param name="map">The Map entity to add.</param>
    Task AddAsync(Map map);

    /// <summary>
    /// Updates an existing map.
    /// </summary>
    /// <param name="map">The Map entity with updated values.</param>
    Task UpdateAsync(Map map);

    /// <summary>
    /// Deletes a map.
    /// </summary>
    /// <param name="map">The Map entity to delete.</param>
    Task DeleteAsync(Map map);
}
