using System.Security.Cryptography.X509Certificates;
using DDDApplication.Application.CQRs.Commands.Requests;
using DDDApplication.Application.CQRs.Commands.Responses;
using Domain.Repositories;
using MediatR;
using Shared.Exceptions;

namespace DDDApplication.Application.CQRs.Commands.Handlers;

public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResponse>
{
    private readonly IUsersRepository _usersRepository;

    public AuthCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<AuthResponse> Handle(AuthCommand request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.FindByEmail(request.Email);

        if (user == null)
        {
            throw new IncorrectInfosException("Email ou senha incorretos.");
        }

        bool isPasswordEqualToHashPassword = CryptoHelper.Crypto.VerifyHashedPassword(user.PasswordHash, request.Password);

        if (!isPasswordEqualToHashPassword)
        {
            throw new IncorrectInfosException("Email ou senha incorretos");
        }

        return new AuthResponse
        {
            Token = "Authorization Token"
        };
    }

}
