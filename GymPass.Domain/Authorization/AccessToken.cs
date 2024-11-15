namespace GymPass.Domain.Authorization;

public class AccessToken
{
    public string Token { get; private set; }
    public string Username { get; private set; }
    public string Role { get; private set; }

    public AccessToken(string token, string username, string role)
    {
        Token = token;
        Username = username;
        Role = role;
    }
}
