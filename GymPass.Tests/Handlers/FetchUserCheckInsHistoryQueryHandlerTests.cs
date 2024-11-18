using FluentAssertions;
using GymPass.Application.CQRs.Queries.Handlers;
using GymPass.Application.CQRs.Queries.Requests;
using GymPass.Application.CQRs.Queries.Responses;
using GymPass.Application.Repositories;
using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using GymPass.Shared.Exceptions;
namespace DDDApplication.Tests.Handlers;

public class FetchUserCheckInsHistoryQueryHandlerTests
{
    private readonly ICheckInsRepository _checkInsRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly FetchUserCheckInsHistoryQueryHandler _sut;

    public FetchUserCheckInsHistoryQueryHandlerTests()
    {
        _checkInsRepository = new InMemoryCheckInsRepository();
        _usersRepository = new InMemoryUsersRepository();
        _sut = new FetchUserCheckInsHistoryQueryHandler(_checkInsRepository, _usersRepository);
    }

    [Fact]
    public async void Should_FetchUserCheckInsHistory_WhenUserExists()
    {
        // Arrange
        User user = await _usersRepository.Create(User.Create(
            null,
            "User",
            "user@email.com",
            "12346",
            null
        ));

        for (int i = 0; i < 10; i++)
        {
            await _checkInsRepository.Create(CheckIn.Create(null, user.Id, $"gym-{i}", null, null));
        }

        // Act
        CancellationTokenSource tokenSource = new();
        CancellationToken token = tokenSource.Token;

        FetchUserCheckInsHistoryResponse response = await _sut.Handle(new FetchUserCheckInsHistoryQuery()
        {   
            Page = 1,
            UserId = user.Id
        }, token);

        var checkIns = response.CheckIns;

        // Arrange
        response.Should().NotBeNull();
        response.CheckIns.Count.Should().Be(10);
    }

    [Fact]
    public async void Should_ThrowExcpetion_WhenUserIdIsUndefined()
    {
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token = tokenSource.Token;

        await Assert.ThrowsAsync<NotFoundRegisterException>(() => _sut.Handle(new FetchUserCheckInsHistoryQuery()
        {
            UserId = "undefined-id"
        }, token));
    }
}