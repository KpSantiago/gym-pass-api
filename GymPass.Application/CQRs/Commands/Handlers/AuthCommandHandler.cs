using GymPass.Domain.Repositories;
using MediatR;
using GymPass.Shared.Exceptions;
using GymPass.Application.CQRs.Commands.Requests;
using GymPass.Application.CQRs.Commands.Responses;
using GymPass.Domain.Authorization;

namespace GymPass.Application.CQRs.Commands.Handlers;

public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly ITokenService _tokenService;

    public AuthCommandHandler(IUsersRepository usersRepository, ITokenService tokenService)
    {
        _usersRepository = usersRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.FindByEmail(request.Email);

        if (user is null)
        {
            throw new IncorrectInfosException("Email ou senha incorretos.");
        }

        bool isPasswordEqualToHashPassword = CryptoHelper.Crypto.VerifyHashedPassword(user.PasswordHash, request.Password);

        if (!isPasswordEqualToHashPassword)
        {
            throw new IncorrectInfosException("Email ou senha incorretos");
        }

        AccessToken accessToken = _tokenService.GenerateToken(user);

        return new AuthResponse
        {
            AccessToken = accessToken
        };
    }

}
