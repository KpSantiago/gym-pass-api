using Domain.Entities;

namespace Domain.Repositories;

public interface IUsersRepository
{
    public Task<User> Create(User user);

    public Task<User?> FindById(string id);

    public Task<User?> FindByEmail(string email);
}
