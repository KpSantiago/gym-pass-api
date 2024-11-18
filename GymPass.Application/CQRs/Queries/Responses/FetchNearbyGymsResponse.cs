using GymPass.Domain.Entities;

namespace GymPass.Application.CQRs.Queries.Responses;

public class FetchNearbyGymsResponse
{
    public List<Gym> Gyms { get; set; } = default!;
}