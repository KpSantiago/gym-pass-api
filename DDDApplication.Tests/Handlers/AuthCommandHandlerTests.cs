using DDDApplication.Application.CQRs.Commands.Handlers;
using DDDApplication.Application.CQRs.Commands.Requests;
using DDDApplication.Application.CQRs.Commands.Responses;
using DDDApplication.Application.Repositories;
<<<<<<< Updated upstream
=======
using DDDApplication.Application.Services;
using Domain.Authorization;
>>>>>>> Stashed changes
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Shared.Exceptions;

namespace DDDApplication.Tests.Handlers;

public class AuthCommandHandlerTests
{
    private readonly IUsersRepository _usersRepository;
<<<<<<< Updated upstream
=======
    private readonly ITokenService _tokenService;
>>>>>>> Stashed changes

    private readonly AuthCommandHandler _sut;

    public AuthCommandHandlerTests()
    {
        _usersRepository = new InMemoryUsersRepository();
<<<<<<< Updated upstream
        _sut = new AuthCommandHandler(_usersRepository);
=======
        _tokenService = new InMemoryTokenService();
        _sut = new AuthCommandHandler(_usersRepository, _tokenService);
>>>>>>> Stashed changes
    }

    [Fact]
    public async void Should_DoAuthentication_WhenEmailAndPasswordAreCorrets()
    {
        await _usersRepository.Create(User.Create(
            id: null,
            name: "User",
            email: "user@gmail.com",
            password: CryptoHelper.Crypto.HashPassword("123456"),
            createdAt: null
        ));

        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        var result = await _sut.Handle(new AuthCommand
        {
            Email = "user@gmail.com",
            Password = "123456"
        }, token);

        result.Should().BeOfType(typeof(AuthResponse));
<<<<<<< Updated upstream
        result.Token.Should().BeOfType(typeof(string));
=======
        result.AccessToken.Should().BeEquivalentTo(new {Username = "User"});
>>>>>>> Stashed changes
    }

    [Fact]
    public async void Should_ThrowAException_WhenEmailOrPasswordAreIncorrets()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        await Assert.ThrowsAsync<IncorrectInfosException>(() => _sut.Handle(new AuthCommand
        {
            Email = "user@gmail.com",
            Password = "123456"
        }, token));
    }
}
