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
        var user = await _context.Users.Include(u => u.Roles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.Email.Equals(email));

        return user;
    }

    public async Task<User?> FindById(string id)
    {
        var result = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        return result;
    }
}