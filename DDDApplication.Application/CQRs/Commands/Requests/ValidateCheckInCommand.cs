using DDDApplication.Application.CQRs.Commands.Responses;
using MediatR;

namespace DDDApplication.Application.CQRs.Commands.Requests;

public class ValidateCheckInCommand : IRequest<ValidateCheckInResponse>
{
    public string CheckInId { get; set; } = default!;
}
