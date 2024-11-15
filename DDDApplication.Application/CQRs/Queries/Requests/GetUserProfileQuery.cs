using System.ComponentModel.DataAnnotations;
using DDDApplication.Application.CQRs.Queries.Responses;
using MediatR;

namespace DDDApplication.Application.CQRs.Queries.Requests;

public class GetUserProfileQuery : IRequest<GetUserProfileResponse>
{
    [Required]
    public string UserId { get; set; } = default!;
}
