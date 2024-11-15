using GymPass.Application.CQRs.Commands.Responses;
using GymPass.Domain.ValueObjects;
using MediatR;

namespace GymPass.Application.CQRs.Commands.Requests;

public class CreateGymCommand : IRequest<CreateGymResponse>
{
    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string Phone { get; set; } = default!;

    public Cordinate Cordinate { get; set; } = default!;
}
