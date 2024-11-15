using GymPass.Domain.Entities;

namespace GymPass.Domain.Authorization;

public interface ITokenService
{
    AccessToken GenerateToken(User user);
}
