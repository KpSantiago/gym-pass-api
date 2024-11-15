using GymPass.Domain.ValueObjects;

namespace GymPass.Application.CQRs.Commands.Responses;

public class CreateGymResponse : GenericResponse
{
    public string Title { get; set; } = default!;
    
    public string Description { get; set; } = default!;

    public Cordinate Cordinate { get; set; } = default!;
}
