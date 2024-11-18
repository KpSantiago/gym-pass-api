using System.Security.Claims;
using GymPass.Shared.Exceptions;

namespace GymPass.API.Middlewares;

public class RolesMiddleware
{
    public static void VerifyRole(string matchRole, IEnumerable<Claim> userClaims)
    {
        if (!userClaims.Any(m => m.Type == "role" && m.Value.Equals(matchRole.ToLower())))
        {
            throw new ForbiddenException("Usuário não é autorizado.");
        }
    }
}