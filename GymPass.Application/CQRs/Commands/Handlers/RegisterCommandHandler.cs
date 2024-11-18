using GymPass.Shared.Exceptions;
using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using GymPass.Application.CQRs.Commands.Requests;
using GymPass.Application.CQRs.Commands.Responses;
using MediatR;

namespace GymPass.Application.CQRs.Commands.Handlers;

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

        if (userAlreadyExists is not null)
        {
            throw new ConflictInfosExcpetion("Usuário já existe.");
        }

        string hashedPassword = CryptoHelper.Crypto.HashPassword(request.Password);

        User newUser = User.Create(
            id: null,
            name: request.Name, email: request.Email,
            password: hashedPassword,
            createdAt: null
        );

        Role role = Role.Create("Admin");
        role.Id = 1;

        newUser.Roles = new List<UserRole>() { UserRole.Create(null, newUser.Id, role.Id) };

        var user = await _usersRepository.Create(newUser);

        return new RegisterResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}
