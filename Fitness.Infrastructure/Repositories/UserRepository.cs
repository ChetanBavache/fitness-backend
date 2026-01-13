using Fitness.Domain.Entities;
using Fitness.Domain.Interfaces;
using Fitness.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FitnessDbContext _context;

    public UserRepository(FitnessDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }
}
