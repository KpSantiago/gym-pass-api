using GymPass.Application.CQRs.Commands.Handlers;
using GymPass.Application.CQRs.Commands.Requests;
using GymPass.Application.CQRs.Commands.Responses;
using GymPass.Application.Repositories;
using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using FluentAssertions;
using GymPass.Shared.Exceptions;

namespace DDDApplication.Tests.Handlers;

public class ValidateCheckInCommandHandlerTests
{
    private readonly ICheckInsRepository _checkInsRepository;
    private readonly ValidateCheckInCommandHandler _sut;

    public ValidateCheckInCommandHandlerTests()
    {
        _checkInsRepository = new InMemoryCheckInsRepository();
        _sut = new ValidateCheckInCommandHandler(_checkInsRepository);
    }

    [Fact]
    public async void Should_ValidateCheckIn_WhenCheckInExists()
    {
        var checkIn = await _checkInsRepository.Create(CheckIn.Create(null, "user-id", "gym-id", null, null));

        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        var result = await _sut.Handle(new ValidateCheckInCommand
        {
            CheckInId = checkIn.Id
        }, token);

        result.Should().BeOfType(typeof(ValidateCheckInResponse));
        result.ValidatedAt.Should().BeBefore(DateTime.Now.AddDays(1));
    }

    [Fact]
    public async void Should_ThrowExceeption_WhenCheckInDoesNotExist()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        await Assert.ThrowsAsync<NotFoundRegisterException>(() => _sut.Handle(new ValidateCheckInCommand
        {
            CheckInId = "undefined"
        }, token));
    }
}
