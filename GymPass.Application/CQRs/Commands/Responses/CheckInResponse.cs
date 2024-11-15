namespace GymPass.Application.CQRs.Commands.Responses;

public class CheckInResponse : GenericResponse
{
    public string User { get; set; } = default!;
    
    public string Gym { get; set; } = default!;
}
