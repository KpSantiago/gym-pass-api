using GymPass.Application.CQRs.Commands.Responses;
using GymPass.Domain.ValueObjects;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace GymPass.Application.CQRs.Commands.Requests;

public class CreateGymCommand : IRequest<CreateGymResponse>
{
    [Required]
    public string Title { get; set; } = default!;

    [Required]
    public string Description { get; set; } = default!;

    [Required]
    public string Phone { get; set; } = default!;

    [Required]
    public Cordinate Cordinate { get; set; } = default!;
}
