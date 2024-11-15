using Domain.Entities;

namespace DDDApplication.Application.CQRs.Queries.Responses;

public class FetchUserCheckInsHistoryResponse
{
    public List<CheckIn> CheckIns { get; set; } = default!;
}
