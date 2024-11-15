using System.ComponentModel.DataAnnotations;
using DDDApplication.Application.CQRs.Queries.Responses;
using MediatR;

namespace DDDApplication.Application.CQRs.Queries.Requests;

public class SeachGymsQuery : IRequest<SearchGymsQueryResponse>
{
    [Required]
    public string Query { get; set; } = default!;

    public int? Page { get; set; } = default!;
}
