using System.Security.Cryptography.X509Certificates;
using Domain.Entities;
using Domain.Repositories;

namespace DDDApplication.Application.Repositories;

public class InMemoryUsersRepository : IUsersRepository
{
    public List<User> Users = new();

    public Task<User> Create(User user)
    {
        Users.Add(user);

        Task<User> result = Task.FromResult(user);

        return result;
    }

    public Task<User?> FindByEmail(string email)
    {
        var result = Users.Find(u => u.Email == email);

        return Task.FromResult(result);
    }

    public Task<User?> FindById(string id)
    {
        var result = Users.Find(u => u.Id == id);

        return Task.FromResult(result);
    }

}
