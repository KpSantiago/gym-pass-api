namespace GymPass.Shared.Exceptions;

public class IncorrectInfosException : RootException
{   
    public IncorrectInfosException(string message) : base(message, 400, "Bad Request") {}
}