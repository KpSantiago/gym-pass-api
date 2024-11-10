using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Repositories;

public class FindManyNearbyParams : Cordinate {}

public interface IGymsRepository
{
    public Task<Gym?> FindById(string id);

    public Task<List<Gym>> SearchMany(string query, int page);

    public Task<List<Gym>> FindManyNearby(FindManyNearbyParams param);

    public Task<Gym> Create(Gym data);

}
