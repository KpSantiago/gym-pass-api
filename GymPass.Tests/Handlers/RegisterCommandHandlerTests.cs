using System.Reflection;
using GymPass.Application.CQRs.Commands.Handlers;
using GymPass.Application.CQRs.Commands.Requests;
using GymPass.Application.CQRs.Commands.Responses;
using GymPass.Application.Repositories;
using GymPass.Shared.Exceptions;
using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using FluentAssertions;
using GymPass.Shared.Exceptions;

namespace DDDApplication.Tests.Handlers;

public class RegisterCommandHandlerTests
{
    private readonly IUsersRepository _usersRepository;
    private readonly RegisterCommandHandler _sut;

    public RegisterCommandHandlerTests()
    {
        _usersRepository = new InMemoryUsersRepository();
        _sut = new RegisterCommandHandler(_usersRepository);
    }

    [Fact]
    public async void Should_DoRegister_WhenAllFieldsAreCorrects()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        var result = await _sut.Handle(new RegisterCommand
        {
            Email = "user@email.com",
            Name = "User",
            Password = "123456"
        }, token);

        result.Should().BeOfType(typeof(RegisterResponse));
        result.Should().BeEquivalentTo(new
        {
            Email = "user@email.com",
            Name = "User"
        });
    }

    [Fact]
    public async void Should_ThrowAnException_WhenUserWithEmailUsedAlreadyExists()
    {
        await _usersRepository.Create(User.Create(
            id: null,
            name: "User 1",
            email: "user@email.com",
            password: "123456",
            createdAt: null
        ));

        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        await Assert.ThrowsAsync<ConflictInfosExcpetion>(() => _sut.Handle(new RegisterCommand
        {
            Email = "user@email.com",
            Name = "User",
            Password = "123456"
        }, token));
    }
}
