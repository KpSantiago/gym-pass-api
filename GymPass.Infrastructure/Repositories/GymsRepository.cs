using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using GymPass.Infrastructure.DB;
using Microsoft.EntityFrameworkCore;

namespace GymPass.Infrastructure.Repositories;

public class GymsRepository : IGymsRepository
{
    private readonly GymPassContext _context;

    public GymsRepository(GymPassContext context)
    {
        _context = context;
    }

    public async Task<Gym> Create(Gym data)
    {
        var result = _context.Gyms.Add(data);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<Gym?> FindById(string id)
    {
        var result = await _context.Gyms.FirstOrDefaultAsync(g => g.Id == id);

        return result;
    }

    public async Task<List<Gym>> FindManyNearby(FindManyNearbyParams param)
    {
        var result = await _context.Gyms.FromSql($@"
            SELECT * from gyms
            WHERE ( 6371 * acos( cos( radians({param.Latitude}) ) * cos( radians( latitude ) ) * cos( radians( longitude ) - radians({param.Longitude}) ) + sin( radians({param.Latitude}) ) * sin( radians( latitude ) ) ) ) <= 10").ToListAsync();

        return result;
    }

    public async Task<List<Gym>> SearchMany(string query, int page)
    {
        var result = await _context.Gyms.Where(g => g.Title.Contains(query)).Skip((page - 1) * page).Take(10).ToListAsync();

        return result;
    }
}
