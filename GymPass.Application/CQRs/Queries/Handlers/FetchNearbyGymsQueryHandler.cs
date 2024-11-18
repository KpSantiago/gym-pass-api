using GymPass.Application.CQRs.Queries.Requests;
using GymPass.Application.CQRs.Queries.Responses;
using GymPass.Domain.Entities;
using GymPass.Domain.Repositories;
using MediatR;

namespace GymPass.Application.CQRs.Queries.Handlers;

public class FetchNearbyGymsQueryHandler : IRequestHandler<FetchNearbyGymsQuery, FetchNearbyGymsResponse>
{
    private readonly IGymsRepository _gymsRepository;

    public FetchNearbyGymsQueryHandler(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }
    
    public async Task<FetchNearbyGymsResponse> Handle(FetchNearbyGymsQuery request, CancellationToken cancellationToken)
    {
        List<Gym> gyms = await _gymsRepository.FindManyNearby(new FindManyNearbyParams()
        {
            Latitude = request.Cordinate.Latitude,
            Longitude = request.Cordinate.Longitude,
        });
        
        return new FetchNearbyGymsResponse() { Gyms = gyms };
    }
}