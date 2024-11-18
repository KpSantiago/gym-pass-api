using GymPass.Application.CQRs.Queries.Handlers;
using GymPass.Application.CQRs.Queries.Responses;
using GymPass.Application.CQRs.Queries.Requests;
using GymPass.Application.Repositories;
using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using GymPass.Domain.ValueObjects;
using FluentAssertions;

namespace DDDApplication.Tests.Handlers;

public class FetchNearbyGymsQueryHandlerTests
{
    private readonly IGymsRepository _gymsRepository;
    private readonly FetchNearbyGymsQueryHandler _sut;

    public FetchNearbyGymsQueryHandlerTests()
    {
        _gymsRepository = new InMemoryGymsRepository();
        _sut = new FetchNearbyGymsQueryHandler(_gymsRepository);
    }

    [Fact]
    public async void Should_FetchNearbyGyms_WhenTheHandlerIsCalled()
    {
        // Arrange
        for (int i = 0; i < 10; i++)
        {
            await _gymsRepository.Create(Gym.Create(
                id: null,
                cordinate: new Cordinate(1001, 1101),
                createdAt: null,
                title: $"Gym - {1}"
            ));
        }

        // Act
        CancellationTokenSource tokenSource = new();
        CancellationToken token = tokenSource.Token;

        FetchNearbyGymsResponse response = await _sut.Handle(new FetchNearbyGymsQuery()
        {
            Cordinate = new Cordinate(1001, 1102)
        }, token);

        // Assert
        response.Should().NotBeNull();
        response.Gyms.Count.Should().Be(10);
    }
}