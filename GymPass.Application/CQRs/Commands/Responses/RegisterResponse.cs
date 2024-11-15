namespace GymPass.Application.CQRs.Commands.Responses;

public class RegisterResponse : GenericResponse
{
    public string Name { get; set; } = default!;
    
    public string Email { get; set; } = default!;
}
