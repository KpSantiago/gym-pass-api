using System.ComponentModel.DataAnnotations;
using DDDApplication.Application.CQRs.Queries.Responses;
using MediatR;

namespace DDDApplication.Application.CQRs.Queries.Requests;

public class FetchUserCheckInsHistoryQuery : IRequest<FetchUserCheckInsHistoryResponse>
{

    [Required]
    public string UserId { get; set; } = default!;
    
    public int? Page { get; set; } = default!;
}
