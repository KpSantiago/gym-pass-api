using DDDApplication.Application.CQRs.Commands.Responses;
using Domain.ValueObjects;
using MediatR;

namespace DDDApplication.Application.CQRs.Commands.Requests;

public class CreateGymCommand : IRequest<CreateGymResponse>
{
    public string Title { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string Phone { get; set; } = default!;

    public Cordinate Cordinate { get; set; } = default!;
}
