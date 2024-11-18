using System.Security.Claims;
using GymPass.Shared.Exceptions;

namespace GymPass.API.Middlewares;

public class RolesMiddleware
{
    public static void VerifyRole(string matchRole, IEnumerable<Claim> userClaims)
    {
        if (userClaims.FirstOrDefault(m => m.Type.Equals(ClaimTypes.Role) && m.Value.ToLower().Equals(matchRole.ToLower())) is null)
        {
            throw new ForbiddenException("Usuário não é autorizado.");
        }
    }
}