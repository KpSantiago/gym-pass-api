using Domain.Entities;

namespace Domain.Authorization;

public interface ITokenService
{
    AccessToken GenerateToken(User user);
}
