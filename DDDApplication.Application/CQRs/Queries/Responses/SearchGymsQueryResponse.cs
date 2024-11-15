using Domain.Entities;

namespace DDDApplication.Application.CQRs.Queries.Responses;

public class SearchGymsQueryResponse
{
    public List<Gym> Gyms { get; set; } = default!;
}
