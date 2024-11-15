using System.ComponentModel.DataAnnotations;
using GymPass.Application.CQRs.Queries.Responses;
using MediatR;

namespace GymPass.Application.CQRs.Queries.Requests;

public class GetUserProfileQuery : IRequest<GetUserProfileResponse>
{
    [Required]
    public string UserId { get; set; } = default!;
}
