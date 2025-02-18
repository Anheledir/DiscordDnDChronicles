using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;
using DiscordDnDChronicles.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscordDnDChronicles.Infrastructure.Repositories;

public class CharacterGroupRepository(AppDbContext context) : ICharacterGroupRepository
{
    public async Task AddAsync(CharacterGroup group)
    {
        await context.CharacterGroups.AddAsync(group);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CharacterGroup group)
    {
        context.CharacterGroups.Remove(group);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CharacterGroup>> GetAllAsync() => await context.CharacterGroups.ToListAsync();

    public async Task<CharacterGroup> GetByIdAsync(int id) => await context.CharacterGroups.FirstOrDefaultAsync(g => g.Id == id);

    public async Task<CharacterGroup> GetByNameAsync(string name) => await context.CharacterGroups.FirstOrDefaultAsync(g => g.Name == name);

    public async Task UpdateAsync(CharacterGroup group)
    {
        context.CharacterGroups.Update(group);
        await context.SaveChangesAsync();
    }
}
