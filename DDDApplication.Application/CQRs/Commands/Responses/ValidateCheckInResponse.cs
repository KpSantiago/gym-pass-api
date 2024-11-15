namespace DDDApplication.Application.CQRs.Commands.Responses;

public class ValidateCheckInResponse : GenericResponse
{
    public DateTime ValidatedAt { get; set; } = default!;
}
