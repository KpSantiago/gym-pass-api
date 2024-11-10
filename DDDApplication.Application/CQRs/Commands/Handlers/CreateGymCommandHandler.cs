using DDDApplication.Application.CQRs.Commands.Requests;
using DDDApplication.Application.CQRs.Commands.Responses;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Repositories;
using MediatR;

namespace DDDApplication.Application.CQRs.Commands.Handlers;

public class CreateGymCommandHandler : IRequestHandler<CreateGymCommand, CreateGymResponse>
{
    public readonly IGymsRepository _gymsRepository;

    public CreateGymCommandHandler(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }

    public async Task<CreateGymResponse> Handle(CreateGymCommand request, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.Create(Gym.Create(
            id: null,
            title: request.Title,
            description: request.Description,
            phone: request.Phone,
            cordinate: new Cordinate(
                latitude: request.Cordinate.Latitude,
                longitude: request.Cordinate.Longitude
            ),
            createdAt: null
        ));

        return new CreateGymResponse
        {
            Id = gym.Id,
            Title = gym.Title,
            Description = gym.Description,
            Cordinate = new Cordinate(gym.Cordinate.Latitude, gym.Cordinate.Longitude)
        };
    }
}
