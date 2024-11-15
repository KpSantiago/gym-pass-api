<<<<<<< Updated upstream
using System.Security.Cryptography.X509Certificates;
using DDDApplication.Application.CQRs.Commands.Requests;
using DDDApplication.Application.CQRs.Commands.Responses;
=======
using DDDApplication.Application.CQRs.Commands.Requests;
using DDDApplication.Application.CQRs.Commands.Responses;
using Domain.Authorization;
>>>>>>> Stashed changes
using Domain.Repositories;
using MediatR;
using Shared.Exceptions;

namespace DDDApplication.Application.CQRs.Commands.Handlers;

public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResponse>
{
    private readonly IUsersRepository _usersRepository;
<<<<<<< Updated upstream

    public AuthCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
=======
    private readonly ITokenService _tokenService;

    public AuthCommandHandler(IUsersRepository usersRepository, ITokenService tokenService)
    {
        _usersRepository = usersRepository;
        _tokenService = tokenService;
>>>>>>> Stashed changes
    }

    public async Task<AuthResponse> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.FindByEmail(request.Email);

<<<<<<< Updated upstream
        if (user == null)
=======
        if (user is null)
>>>>>>> Stashed changes
        {
            throw new IncorrectInfosException("Email ou senha incorretos.");
        }

        bool isPasswordEqualToHashPassword = CryptoHelper.Crypto.VerifyHashedPassword(user.PasswordHash, request.Password);

        if (!isPasswordEqualToHashPassword)
        {
            throw new IncorrectInfosException("Email ou senha incorretos");
        }

<<<<<<< Updated upstream
        return new AuthResponse
        {
            Token = "Authorization Token"
=======
        AccessToken accessToken = _tokenService.GenerateToken(user);

        return new AuthResponse
        {
            AccessToken = accessToken
>>>>>>> Stashed changes
        };
    }

}
