using System.ComponentModel.DataAnnotations;
using DDDApplication.Application.CQRs.Commands.Responses;
using MediatR;

namespace DDDApplication.Application.CQRs.Commands.Requests;

public class CheckInCommand : IRequest<CheckInResponse>
{
    [Required]
    public string UserId {get; set;} = default!;

    [Required]
    public string GymId {get; set;} = default!;
<<<<<<< Updated upstream
=======

    [Required]
    public double Latitude {get; set;} = default!;

    [Required]
    public double Longitude {get; set;} = default!;
>>>>>>> Stashed changes
}
