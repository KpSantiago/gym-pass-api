using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using GymPass.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace GymPass.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly GymPassContext _context;
    
    public UsersRepository(GymPassContext context)
    {
        _context = context;
    }

    public async Task<User> Create(User user)
    {
        var result = _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<User?> FindByEmail(string email)
    {
        var users = await _context.Users.ToListAsync();

        return users.Find(u => u.Email == email);
    }

    public async Task<User?> FindById(string id)
    {
        var result = await _context.Users.ToListAsync();

        return result.Find(u => u.Id == id);
    }
}