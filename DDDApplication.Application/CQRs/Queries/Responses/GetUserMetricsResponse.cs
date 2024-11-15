namespace DDDApplication.Application.CQRs.Queries.Responses;

public class GetUserMetricsResponse : GenericResponse
{
    public int Metrics { get; set; } = default!;
}
