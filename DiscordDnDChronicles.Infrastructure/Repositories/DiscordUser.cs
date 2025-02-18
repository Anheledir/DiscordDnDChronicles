using DiscordDnDChronicles.Core.Domain.Models;
using DiscordDnDChronicles.Core.Domain.Repositories.Interfaces;
using DiscordDnDChronicles.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DiscordDnDChronicles.Infrastructure.Repositories;

public class DiscordUserRepository(AppDbContext context) : IDiscordUserRepository
{
    public async Task AddAsync(DiscordUser discordUser)
    {
        await context.DiscordUsers.AddAsync(discordUser);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(DiscordUser discordUser)
    {
        context.DiscordUsers.Remove(discordUser);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<DiscordUser>> GetAllAsync() => await context.DiscordUsers.ToListAsync();

    public async Task<DiscordUser> GetByDiscordUserIdAsync(string discordUserId) => await context.DiscordUsers.FirstOrDefaultAsync(u => u.DiscordUserId == discordUserId);

    public async Task<DiscordUser> GetByIdAsync(int id) => await context.DiscordUsers.FirstOrDefaultAsync(u => u.Id == id);

    public async Task UpdateAsync(DiscordUser discordUser)
    {
        context.DiscordUsers.Update(discordUser);
        await context.SaveChangesAsync();
    }
}
