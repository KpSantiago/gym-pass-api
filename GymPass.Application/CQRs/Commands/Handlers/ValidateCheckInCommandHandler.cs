using GymPass.Domain.Repositories;
using GymPass.Application.CQRs.Commands.Requests;
using GymPass.Application.CQRs.Commands.Responses;
using MediatR;
using GymPass.Shared.Exceptions;

namespace GymPass.Application.CQRs.Commands.Handlers;

public class ValidateCheckInCommandHandler : IRequestHandler<ValidateCheckInCommand, ValidateCheckInResponse>
{
    private readonly ICheckInsRepository _checkInsRepository;

    public ValidateCheckInCommandHandler(ICheckInsRepository checkInsRepository)
    {
        _checkInsRepository = checkInsRepository;
    }

    public async Task<ValidateCheckInResponse> Handle(ValidateCheckInCommand request, CancellationToken cancellationToken)
    {
        var checkIn = await _checkInsRepository.FindById(request.CheckInId);

        if (checkIn is null)
        {
            throw new NotFoundRegisterException("Este check-in não existe.");
        }

        DateTime today = DateTime.UtcNow;
        checkIn.ValidatedAt = today;

        var result = await _checkInsRepository.Update(checkIn);

        return new ValidateCheckInResponse
        {
            Id = result.Id,
            ValidatedAt = today
        };
    }
}
