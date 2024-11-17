using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using GymPass.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace GymPass.Infrastructure.Repositories;

public class CheckInsRepository : ICheckInsRepository
{
    private readonly GymPassContext _context;

    public CheckInsRepository(GymPassContext context)
    {
        _context = context;
    }

    public async Task<CheckIn?> FindByUserIdOnDate(string userId, DateTime date)
    {
        var result = await _context.CheckIns.ToListAsync();

        return result.Find(c => c.UserId == userId && c.CreatedAt.Day == date.Day);
    }

    public async Task<List<CheckIn>> FindManyByUserId(string userId, int page)
    {
        var result = await _context.CheckIns.ToListAsync();

        return result.Where(c => c.UserId == userId).Take(page * 10).ToList();
    }

    public async Task<CheckIn?> FindById(string id)
    {
        var result = await _context.CheckIns.ToListAsync();

        return result.Find(c => c.Id == id);
    }

    public async Task<int> CountCheckInsByUserId(string userId)
    {
        var result = await _context.CheckIns.ToListAsync();

        return result.Count(c => c.UserId == userId);
    }

    public async Task<CheckIn> Create(CheckIn checkIn)
    {
        var result =  _context.CheckIns.Add(checkIn);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<CheckIn> Update(CheckIn checkIn)
    {
        var result = _context.CheckIns.Update(checkIn);
        await _context.SaveChangesAsync();

        return result.Entity;
    }
}
