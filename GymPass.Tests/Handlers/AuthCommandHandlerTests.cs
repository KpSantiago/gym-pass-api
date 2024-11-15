using GymPass.Application.CQRs.Commands.Handlers;
using GymPass.Application.CQRs.Commands.Requests;
using GymPass.Application.CQRs.Commands.Responses;
using GymPass.Application.Repositories;
using GymPass.Application.Services;
using GymPass.Domain.Authorization;
using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using FluentAssertions;
using GymPass.Shared.Exceptions;

namespace DDDApplication.Tests.Handlers;

public class AuthCommandHandlerTests
{
    private readonly IUsersRepository _usersRepository;
    private readonly ITokenService _tokenService;

    private readonly AuthCommandHandler _sut;

    public AuthCommandHandlerTests()
    {
        _usersRepository = new InMemoryUsersRepository();
        _tokenService = new InMemoryTokenService();
        _sut = new AuthCommandHandler(_usersRepository, _tokenService);
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
        result.AccessToken.Should().BeEquivalentTo(new {Username = "User"});
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
