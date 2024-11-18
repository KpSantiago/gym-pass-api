using GymPass.Application.CQRs.Queries.Responses;
using GymPass.Domain.ValueObjects;
using MediatR;

namespace GymPass.Application.CQRs.Queries.Requests;

public class FetchNearbyGymsQuery : IRequest<FetchNearbyGymsResponse>
{
    public Cordinate Cordinate { get; set; } = default!;
}