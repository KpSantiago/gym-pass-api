using GymPass.Application.CQRs.Queries.Handlers;
using GymPass.Application.CQRs.Queries.Responses;
using GymPass.Application.CQRs.Queries.Requests;
using GymPass.Application.Repositories;
using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using FluentAssertions;
using GymPass.Shared.Exceptions;

namespace DDDApplication.Tests.Handlers;

public class GetUserProfileQueryHandlerTests
{
    private readonly IUsersRepository _usersRepository;
    private readonly GetUserProfileQueryHandler _sut;

    public GetUserProfileQueryHandlerTests() { 
        _usersRepository = new InMemoryUsersRepository();
        _sut = new GetUserProfileQueryHandler(_usersRepository);
    }

    [Fact]
    public async void Should_GetUserProfile_WhenUserIsLogged()
    {
        User user = await _usersRepository.Create(User.Create(
            null,
            "User",
            "user@email.com",
            CryptoHelper.Crypto.HashPassword("123456"),
            null
        ));

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token = tokenSource.Token;

        GetUserProfileResponse result = await _sut.Handle(new GetUserProfileQuery()
        {
            UserId = user.Id
        }, token);
        
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new { Name = "User", Email = "user@email.com" });
    }

    [Fact]
    public async void Should_ThrowExcpetion_WhenUserIdIsUndefined()
    {
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token = tokenSource.Token;

        await Assert.ThrowsAsync<NotFoundRegisterException>(() => _sut.Handle(new GetUserProfileQuery()
        {
            UserId = "undefined-id"
        }, token));
    }
}
