using System.Runtime.CompilerServices;
using DDDApplication.Application.CQRs.Commands.Handlers;
using DDDApplication.Application.CQRs.Commands.Requests;
using DDDApplication.Application.CQRs.Commands.Responses;
using DDDApplication.Application.Repositories;
using Domain.Repositories;
using Domain.ValueObjects;
using FluentAssertions;

namespace DDDApplication.Tests.Handlers;

public class CreateGymCommandHandlerTests
{
    private readonly IGymsRepository _gymsRepository;
    private readonly CreateGymCommandHandler _sut;

    public CreateGymCommandHandlerTests()
    {
        _gymsRepository = new InMemoryGymsRepository();
        _sut = new CreateGymCommandHandler(_gymsRepository);
    }

    [Fact]
    public async void Should_CreateGym_WhenAllInfosAreCorrects()
    {
        var cts = new CancellationTokenSource();
        CancellationToken token = cts.Token;

        var result = await _sut.Handle(new CreateGymCommand
        {
            Cordinate = new Cordinate(0, 0),
            Title = "Gym",
            Description = "",
            Phone = "",
        }, token);

        result.Should().BeOfType(typeof(CreateGymResponse));
        result.Should().BeEquivalentTo(new
        {
            Title = "Gym",
            Cordinate = new
            {
                Latitude = 0,
                Longitude = 0
            }
        });
    }
}
