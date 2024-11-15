using GymPass.Domain.Authorization;

namespace GymPass.Application.CQRs.Commands.Responses;

public class AuthResponse
{
    public AccessToken AccessToken { get; set; } = default!;
}
