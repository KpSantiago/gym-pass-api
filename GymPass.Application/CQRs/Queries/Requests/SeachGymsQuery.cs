using System.ComponentModel.DataAnnotations;
using GymPass.Application.CQRs.Queries.Responses;
using MediatR;

namespace GymPass.Application.CQRs.Queries.Requests;

public class SeachGymsQuery : IRequest<SearchGymsQueryResponse>
{
    [Required]
    public string Query { get; set; } = default!;

    public int? Page { get; set; } = default!;
}
