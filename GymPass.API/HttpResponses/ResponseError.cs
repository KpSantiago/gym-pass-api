namespace GymPass.API.HttpResponses;

public class ResponseError
{ 
    public int Status { get; set; }
    public string Error { get; set; } = default!;
    public string Message { get; set; } = default!;
    public DateTime Timestamp { get; set; }
}