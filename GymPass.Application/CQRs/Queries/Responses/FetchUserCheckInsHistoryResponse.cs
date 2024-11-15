using GymPass.Domain.Entities;

namespace GymPass.Application.CQRs.Queries.Responses;

public class FetchUserCheckInsHistoryResponse
{
    public List<CheckIn> CheckIns { get; set; } = default!;
}
