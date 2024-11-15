using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;

namespace GymPass.Application.Repositories;

public class InMemoryGymsRepository : IGymsRepository
{
    public List<Gym> items = new();

    public Task<Gym> Create(Gym data)
    {
        items.Add(data);

        return Task.FromResult(data);
    }

    public Task<Gym?> FindById(string id)
    {
        var result = Task.FromResult(items.Find(i => i.Id == id));

        return result;
    }

    public Task<List<Gym>> FindManyNearby(FindManyNearbyParams param)
    {
        var result = Task.FromResult(items.Where(i => i.Cordinate.Latitude <= param.Latitude && i.Cordinate.Longitude <= param.Longitude).ToList());

        return result;
    }

    public Task<List<Gym>> SearchMany(string query, int page)
    {
        var result = Task.FromResult(items.Where(i => i.Title.Contains(query)).Take(1 * 10).ToList());

        return result;
    }

}
