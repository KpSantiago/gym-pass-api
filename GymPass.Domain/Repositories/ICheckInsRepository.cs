using GymPass.Domain.Entities;

namespace GymPass.Domain.Repositories;

public interface ICheckInsRepository
{
    public Task<CheckIn?> FindByUserIdOnDate(string userId, DateTime date);

    public Task<List<CheckIn>> FindManyByUserId(string userId, int page);

    public Task<CheckIn?> FindById(string id);

    public Task<int> CountCheckInsByUserId(string userId);

    public Task<CheckIn> Create(CheckIn checkIn);

    public Task<CheckIn> Update(CheckIn checkIn);

}
