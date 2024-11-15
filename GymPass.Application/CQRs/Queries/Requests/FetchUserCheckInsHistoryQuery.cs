using System.ComponentModel.DataAnnotations;
using GymPass.Application.CQRs.Queries.Responses;
using MediatR;

namespace GymPass.Application.CQRs.Queries.Requests;

public class FetchUserCheckInsHistoryQuery : IRequest<FetchUserCheckInsHistoryResponse>
{

    [Required]
    public string UserId { get; set; } = default!;
    
    public int? Page { get; set; } = default!;
}
