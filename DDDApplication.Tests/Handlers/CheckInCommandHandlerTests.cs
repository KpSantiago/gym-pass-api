using DDDApplication.Application.CQRs.Commands.Handlers;
using DDDApplication.Application.CQRs.Commands.Requests;
using DDDApplication.Application.CQRs.Commands.Responses;
using DDDApplication.Application.Repositories;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using FluentAssertions;

namespace DDDApplication.Tests.Handlers;

public class CheckInCommandHandlerTests
{
    private readonly IUsersRepository _usersRaepository;
    private readonly IGymsRepository _gymsRaepository;
    private readonly ICheckInsRepository _checkInsRaepository;
    private readonly CheckInCommandHandler _sut;
    public CheckInCommandHandlerTests()
    {
        _usersRaepository = new InMemoryUsersRepository();
        _gymsRaepository = new InMemoryGymsRepository();
        _checkInsRaepository = new InMemoryCheckInsRepository();
        _sut = new CheckInCommandHandler(_checkInsRaepository, _gymsRaepository, _usersRaepository);
    }

    [Fact]
    public async void Should_DoCheckIn_WhenUserIsAbleToDoIt()
    {
        var gym = await _gymsRaepository.Create(Gym.Create(null, new Cordinate(0, 0), null, "Gym"));
        var user = await _usersRaepository.Create(User.Create(null, "User", "user@email.com", "123456", null));

        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        var result = await _sut.Handle(new CheckInCommand
        {
            GymId = gym.Id,
            UserId = user.Id
        }, token);

        result.Should().BeOfType(typeof(CheckInResponse));
        result.Should().BeEquivalentTo(new
        {
            User = "User",
            Gym = "Gym"
        });
    }

    [Fact]
    public async void Should_ThrowException_WhenUserOrGymDoesNotExist()
    {
        var user = await _usersRaepository.Create(User.Create(null, "User", "user@email.com", "123456", null));

        await _checkInsRaepository.Create(CheckIn.Create(null, user.Id, "gymId", null, null));

        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        await Assert.ThrowsAsync<ArgumentException>(() => _sut.Handle(new CheckInCommand
        {
            GymId = "gym-id",
            UserId = user.Id
        }, token));
    }

    [Fact]
    public async void Should_ThrowException_WhenUserAlreadyHasACheckIn()
    {
        var user = await _usersRaepository.Create(User.Create(null, "User", "user@email.com", "123456", null));

        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        await Assert.ThrowsAsync<ArgumentException>(() => _sut.Handle(new CheckInCommand
        {
            GymId = "undefined",
            UserId = user.Id
        }, token));
    }
}
