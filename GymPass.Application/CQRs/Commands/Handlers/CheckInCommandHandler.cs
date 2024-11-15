using GymPass.Shared.Exceptions;
using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using GymPass.Domain.ValueObjects;
using GymPass.Application.CQRs.Commands.Requests;
using GymPass.Application.CQRs.Commands.Responses;
using GymPass.Application.Utils;
using MediatR;
using GymPass.Shared.Exceptions;

namespace GymPass.Application.CQRs.Commands.Handlers;

public class CheckInCommandHandler : IRequestHandler<CheckInCommand, CheckInResponse>
{
    private readonly ICheckInsRepository _checkInsRepository;
    private readonly IGymsRepository _gymsRepository;
    private readonly IUsersRepository _usersRepository;

    public CheckInCommandHandler(ICheckInsRepository checkInsRepository, IGymsRepository gymsRepository, IUsersRepository usersRepository)
    {
        _checkInsRepository = checkInsRepository;
        _gymsRepository = gymsRepository;
        _usersRepository = usersRepository;
    }

    public async Task<CheckInResponse> Handle(CheckInCommand request, CancellationToken cancellationToken)
    {
        var doesUserExist = await _usersRepository.FindById(request.UserId);

        if (doesUserExist == null)
        {
            throw new NotFoundRegisterException("Usuário não existe.");
        }

        var doesGymExist = await _gymsRepository.FindById(request.GymId);

        if (doesGymExist == null)
        {
            throw new NotFoundRegisterException("A academia não existe.");
        }

        DateTime today = DateTime.Now;

        var doesUserAlreadyHaveACheckInToday = await _checkInsRepository.FindByUserIdOnDate(request.UserId, today);

        if (doesUserAlreadyHaveACheckInToday is not null)
        {
            throw new ConflictInfosExcpetion("Usuário já tem um check-in hoje");
        }

        double distance = GetDistanceBetweenCordinatesUtil.GetDistance(
            new Cordinate(request.Latitude, request.Longitude),
            doesGymExist.Cordinate
        );

        double MAX_DISTANCE = 0.1D;

        if(distance > MAX_DISTANCE)
        {
            throw new IncorrectInfosException("Distância máxima para check-in excedida.");
        }

        var result = await _checkInsRepository.Create(CheckIn.Create(
            id: null,
            userId: request.UserId,
            gymId: request.GymId,
            validatedAt: null,
            createdAt: today
        ));

        return new CheckInResponse
        {
            Id = result.Id,
            Gym = doesGymExist.Title,
            User = doesUserExist.Name
        };
    }

}
