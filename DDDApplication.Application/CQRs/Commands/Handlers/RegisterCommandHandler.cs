using DDDApplication.Application.CQRs.Commands.Requests;
using DDDApplication.Application.CQRs.Commands.Responses;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace DDDApplication.Application.CQRs.Commands.Handlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly IUsersRepository _usersRepository;
    public RegisterCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userAlreadyExists = await _usersRepository.FindByEmail(request.Email);

        if (userAlreadyExists != null)
        {
            throw new ArgumentException("Usuário já existe.");
        }

        string hashedPassword = CryptoHelper.Crypto.HashPassword(request.Password);

        User newUser = User.Create(
            id: null,
            name: request.Name, email: request.Email,
            password: hashedPassword,
            createdAt: null
        );

        var user = await _usersRepository.Create(newUser);

        return new RegisterResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}
