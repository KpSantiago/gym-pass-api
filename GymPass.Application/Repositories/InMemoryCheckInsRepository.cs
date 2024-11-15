using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;

namespace GymPass.Application.Repositories;

public class InMemoryCheckInsRepository : ICheckInsRepository
{
    public List<CheckIn> items = new();

    public Task<int> CountCheckInsByUserId(string userId)
    {
        var result = items.Count(i => i.UserId == userId);

        return Task.FromResult(result);
    }

    public Task<CheckIn> Create(CheckIn checkIn)
    {
        items.Add(checkIn);

        return Task.FromResult(checkIn);
    }

    public Task<CheckIn?> FindById(string id)
    {
        var result = items.Find(i => i.Id == id);

        return Task.FromResult(result);
    }

    public Task<CheckIn?> FindByUserIdOnDate(string userId, DateTime date)
    {
        var result = items.Find(i => i.UserId == userId && i.CreatedAt.Day == date.Day);

        return Task.FromResult(result);
    }

    public Task<List<CheckIn>> FindManyByUserId(string userId, int page)
    {
        var result = items.Where(i => i.Id == userId).Take(1 * 10).ToList();

        return Task.FromResult(result);
    }

    public Task<CheckIn> Update(CheckIn checkIn)
    {
        var index = items.FindIndex(i => i.Id == checkIn.Id);

        items[index] = checkIn;

        return Task.FromResult(checkIn);
    }

}
