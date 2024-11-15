namespace GymPass.Application.CQRs.Queries.Responses;

public class GetUserProfileResponse : GenericResponse
{
    public string Name {get; set;} = default!;
    
    public string Email {get; set;} = default!;
}
