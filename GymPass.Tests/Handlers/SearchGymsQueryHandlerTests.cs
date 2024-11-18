using GymPass.Application.CQRs.Queries.Handlers;
using GymPass.Application.CQRs.Queries.Responses;
using GymPass.Application.CQRs.Queries.Requests;
using GymPass.Application.Repositories;
using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using GymPass.Domain.ValueObjects;
using FluentAssertions;

namespace DDDApplication.Tests.Handlers;

public class SearchGymsQueryHandlerTests
{
    private readonly IGymsRepository _gymRepository;
    private readonly SearchGymsQueryHandler _sut;

    public SearchGymsQueryHandlerTests() {
        _gymRepository = new InMemoryGymsRepository();
        _sut = new SearchGymsQueryHandler(_gymRepository);
    }

    [Fact]
    public async void Should_ReturnAListOfGyms_WhenThemAreSearched()
    {
        // Arrange
        for (int i = 0; i < 20; i++)
        {
            await _gymRepository.Create(Gym.Create(
                null,
                new Cordinate(100, 100),
                null,
                $"Test gym - {i}"
           ));
        }

        // Act
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        CancellationToken token = tokenSource.Token;

        SearchGymsQueryResponse response = await _sut.Handle(new SearchGymsQuery()
        {
            Page = 2,
            Query = "Test gym"
        }, token);

        // Assert
        response.Should().NotBeNull();
        response.Gyms.Count.Should().Be(10);
        response.Gyms[9].Should().BeEquivalentTo(new { Title = "Test gym - 19" });
    }
}