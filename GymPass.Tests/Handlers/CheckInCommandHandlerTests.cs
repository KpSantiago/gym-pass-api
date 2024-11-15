using GymPass.Application.CQRs.Commands.Handlers;
using GymPass.Application.CQRs.Commands.Requests;
using GymPass.Application.CQRs.Commands.Responses;
using GymPass.Application.Repositories;
using GymPass.Shared.Exceptions;
using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using GymPass.Domain.ValueObjects;
using FluentAssertions;
using GymPass.Shared.Exceptions;

namespace DDDApplication.Tests.Handlers;

public class CheckInCommandHandlerTests
{
    private readonly IUsersRepository _usersRepository;
    private readonly IGymsRepository _gymsRepository;
    private readonly ICheckInsRepository _checkInsRepository;
    private readonly CheckInCommandHandler _sut;
    public CheckInCommandHandlerTests()
    {
        _usersRepository = new InMemoryUsersRepository();
        _gymsRepository = new InMemoryGymsRepository();
        _checkInsRepository = new InMemoryCheckInsRepository();
        _sut = new CheckInCommandHandler(_checkInsRepository, _gymsRepository, _usersRepository);
    }

    [Fact]
    public async void Should_DoCheckIn_WhenUserIsAbleToDoIt()
    {
        var gym = await _gymsRepository.Create(Gym.Create(null, new Cordinate(0, 0), null, "Gym"));
        var user = await _usersRepository.Create(User.Create(null, "User", "user@email.com", "123456", null));

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
        var user = await _usersRepository.Create(User.Create(null, "User", "user@email.com", "123456", null));

        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        await Assert.ThrowsAsync<NotFoundRegisterException>(() => _sut.Handle(new CheckInCommand
        {
            GymId = "undefined",
            UserId = user.Id
        }, token));
    }

    [Fact]
    public async void Should_ThrowException_WhenUserAlreadyHasACheckIn()
    {
        var gym = await _gymsRepository.Create(Gym.Create(null, new Cordinate(0, 0), null, "Gym"));
        var user = await _usersRepository.Create(User.Create(null, "User", "user@email.com", "123456", null));

        var AnotherGym = await _gymsRepository.Create(Gym.Create(null, new Cordinate(0, 0), null, "Gym"));

        await _checkInsRepository.Create(CheckIn.Create(null, user.Id, gym.Id, null, null));

        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        await Assert.ThrowsAsync<ConflictInfosExcpetion>(() => _sut.Handle(new CheckInCommand
        {
            GymId = AnotherGym.Id,
            UserId = user.Id
        }, token));
    }

    [Fact]
    public async void Should_ThrowException_WhenMaxDistanceBetweenUserAndGymIsExceded()
    {
        var gym = await _gymsRepository.Create(Gym.Create(null, new Cordinate(-4.9639092, -39.0199046), null, "Super Saya Gym"));
        var user = await _usersRepository.Create(User.Create(null, "User", "user@email.com", "1234", null));

        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        await Assert.ThrowsAsync<IncorrectInfosException>(() => _sut.Handle(new CheckInCommand
        {
            GymId = gym.Id,
            UserId = user.Id,
            Latitude = -49672442,
            Longitude = -3903147.20
        }, token));
    }
}
