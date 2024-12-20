using GymPass.Application.Utils;
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
        
        var result = Task.FromResult(items.Where(i =>
        {
            double distance = GetDistanceBetweenCordinatesUtil.GetDistance(param, i.Cordinate);

            return distance < 10;
        }).ToList());

        return result;
    }

    public Task<List<Gym>> SearchMany(string query, int page)
    {
        var result = Task.FromResult(items.Where(i => i.Title.Contains(query)).Skip((page - 1) * 10).Take(10).ToList());

        return result;
    }

}
