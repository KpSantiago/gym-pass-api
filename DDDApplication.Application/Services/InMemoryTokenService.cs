using Domain.Authorization;
using Domain.Entities;

namespace DDDApplication.Application.Services;

public class InMemoryTokenService : ITokenService
{
    public AccessToken GenerateToken(User user)
    {
        return new AccessToken(token: $"{user.Name}-{user.Id}", username: user.Name, role: "Client");
    }
}