using GymPass.Application.CQRs.Queries.Requests;
using GymPass.Application.CQRs.Queries.Responses;
using GymPass.Domain.Repositories;
using MediatR;

namespace GymPass.Application.CQRs.Queries.Handlers;

public class GetUserMetricsQueryHandler : IRequestHandler<GetUserMetricsQuery, GetUserMetricsResponse>
{
    private readonly ICheckInsRepository _checkInsRepository;

    public GetUserMetricsQueryHandler(ICheckInsRepository checkInsRepository)
    {
        _checkInsRepository = checkInsRepository;
    }

    public async Task<GetUserMetricsResponse> Handle(GetUserMetricsQuery request, CancellationToken cancellationToken)
    {
        int metrics = await _checkInsRepository.CountCheckInsByUserId(request.UserId);

        return new GetUserMetricsResponse {
            Id = request.UserId,
            Metrics = metrics
        };
    }
}
