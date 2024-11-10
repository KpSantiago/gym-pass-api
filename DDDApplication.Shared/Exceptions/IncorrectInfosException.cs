namespace Shared.Exceptions;

public class IncorrectInfosException : ArgumentException
{
    public IncorrectInfosException(string message) : base(message: message) { }
}
