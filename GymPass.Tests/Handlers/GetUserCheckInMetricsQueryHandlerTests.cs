using GymPass.Application.CQRs.Queries.Handlers;
using GymPass.Application.CQRs.Queries.Responses;
using GymPass.Application.CQRs.Queries.Requests;   
using GymPass.Application.Repositories;
using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using FluentAssertions;
using GymPass.Shared.Exceptions;

namespace DDDApplication.Tests.Handlers;

public class GetUserCheckInMetricsQueryHandlerTests
{
    private readonly ICheckInsRepository _checkInsRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly GetUserMetricsQueryHandler _sut;

    public GetUserCheckInMetricsQueryHandlerTests()
    {
        _checkInsRepository = new InMemoryCheckInsRepository();
        _usersRepository = new InMemoryUsersRepository();
        _sut = new GetUserMetricsQueryHandler(_checkInsRepository, _usersRepository);
    }

    [Fact]
    public async void Should_GetUserCheckInsMetrics_WhenUserExists()
    {
        // Arrange 
        User user = await _usersRepository.Create(User.Create(
            null,
            "User",
            "user@email.com",
            CryptoHelper.Crypto.HashPassword("1234"),
            null
        ));

        for (int i = 0; i < 10; i++)
        {
            await _checkInsRepository.Create(CheckIn.Create(null, user.Id, $"gym-{i}", null, null));
        }

        // Act
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token = tokenSource.Token;

        GetUserMetricsResponse result = await _sut.Handle(new GetUserMetricsQuery() {
            UserId = user.Id
        }, token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(new { user.Id, Metrics = 10 });
    }

    [Fact]
    public async void Should_ThrowExcpetion_WhenUserIdIsUndefined()
    {
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token = tokenSource.Token;

        await Assert.ThrowsAsync<NotFoundRegisterException>(() => _sut.Handle(new GetUserMetricsQuery()
        {
            UserId = "undefined-id"
        }, token));
    }
}