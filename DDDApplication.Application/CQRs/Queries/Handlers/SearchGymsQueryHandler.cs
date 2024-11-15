using DDDApplication.Application.CQRs.Queries.Requests;
using DDDApplication.Application.CQRs.Queries.Responses;
using Domain.Repositories;
using MediatR;

namespace DDDApplication.Application.CQRs.Queries.Handlers;

public class SearchGymsQueryHandler : IRequestHandler<SeachGymsQuery, SearchGymsQueryResponse>
{
    private readonly IGymsRepository _gymsRepository;

    public SearchGymsQueryHandler(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }

    public async Task<SearchGymsQueryResponse> Handle(SeachGymsQuery request, CancellationToken cancellationToken)
    {
        var gyms = await _gymsRepository.SearchMany(request.Query, request.Page == null || request.Page == 0 ? 1 : request.Page.Value);

        return new SearchGymsQueryResponse {
            Gyms = gyms
        };
    }
}
