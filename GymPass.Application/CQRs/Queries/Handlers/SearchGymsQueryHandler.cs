using GymPass.Application.CQRs.Queries.Requests;
using GymPass.Application.CQRs.Queries.Responses;
using GymPass.Domain.Repositories;
using MediatR;

namespace GymPass.Application.CQRs.Queries.Handlers;

public class SearchGymsQueryHandler : IRequestHandler<SearchGymsQuery, SearchGymsQueryResponse>
{
    private readonly IGymsRepository _gymsRepository;

    public SearchGymsQueryHandler(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }

    public async Task<SearchGymsQueryResponse> Handle(SearchGymsQuery request, CancellationToken cancellationToken)
    {
        var gyms = await _gymsRepository.SearchMany(request.Query, request.Page == null || request.Page == 0 ? 1 : request.Page.Value);

        return new SearchGymsQueryResponse {
            Gyms = gyms
        };
    }
}
