namespace GymPass.Shared.Exceptions;

public class ForbiddenException : RootException
{
    public ForbiddenException(string message) : base(message, 403, "Forbidden")
    {}
}