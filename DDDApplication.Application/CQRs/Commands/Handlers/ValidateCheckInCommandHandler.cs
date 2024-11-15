using DDDApplication.Application.CQRs.Commands.Requests;
using DDDApplication.Application.CQRs.Commands.Responses;
using Domain.Repositories;
using MediatR;
using Shared.Exceptions;

namespace DDDApplication.Application.CQRs.Commands.Handlers;

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

<<<<<<< Updated upstream
        if (checkIn == null)
=======
        if (checkIn is null)
>>>>>>> Stashed changes
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
