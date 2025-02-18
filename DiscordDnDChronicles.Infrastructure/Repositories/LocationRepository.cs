using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;
using DiscordDnDChronicles.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscordDnDChronicles.Infrastructure.Repositories;

public class LocationRepository(AppDbContext _context) : ILocationRepository
{
    public async Task AddAsync(Location location)
    {
        await _context.Locations.AddAsync(location);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Location location)
    {
        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Location>> GetAllAsync() =>
        await _context.Locations.Include(l => l.Maps).ToListAsync();

    public async Task<Location> GetByIdAsync(int id) =>
        await _context.Locations.Include(l => l.Maps).FirstOrDefaultAsync(l => l.Id == id);

    public async Task<IEnumerable<Location>> GetByCampaignIdAsync(int campaignId) =>
        await _context.Locations.Include(l => l.Maps)
                                .Where(l => l.CampaignId == campaignId)
                                .ToListAsync();

    public async Task<Location> GetBySlugAsync(string slug) =>
        await _context.Locations.Include(l => l.Maps)
                                .FirstOrDefaultAsync(l => l.Slug == slug);

    public async Task UpdateAsync(Location location)
    {
        _context.Locations.Update(location);
        await _context.SaveChangesAsync();
    }
}
