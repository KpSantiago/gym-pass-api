using GymPass.Application.CQRs.Commands.Responses;
using MediatR;

namespace GymPass.Application.CQRs.Commands.Requests;

public class ValidateCheckInCommand : IRequest<ValidateCheckInResponse>
{
    public string CheckInId { get; set; } = default!;
}
