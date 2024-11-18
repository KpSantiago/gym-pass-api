namespace GymPass.API.HttpResponses;

public class ResponseError
{ 
    public int status { get; set; }
    public string error { get; set; } = default!;
    public string message { get; set; } = default!;
    public DateTime timestamp { get; set; }
}