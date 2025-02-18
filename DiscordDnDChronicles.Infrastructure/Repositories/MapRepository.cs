using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;
using DiscordDnDChronicles.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscordDnDChronicles.Infrastructure.Repositories;

public class MapRepository(AppDbContext _context) : IMapRepository
{
    public async Task AddAsync(Map map)
    {
        await _context.Maps.AddAsync(map);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Map map)
    {
        _context.Maps.Remove(map);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Map>> GetAllAsync() =>
        await _context.Maps.ToListAsync();

    public async Task<Map> GetByIdAsync(int id) =>
        await _context.Maps.FirstOrDefaultAsync(m => m.Id == id);

    public async Task<IEnumerable<Map>> GetByLocationIdAsync(int locationId) =>
        await _context.Maps.Where(m => m.LocationId == locationId).ToListAsync();

    public async Task UpdateAsync(Map map)
    {
        _context.Maps.Update(map);
        await _context.SaveChangesAsync();
    }
}
