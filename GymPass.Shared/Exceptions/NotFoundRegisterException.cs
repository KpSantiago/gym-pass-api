namespace GymPass.Shared.Exceptions;

public class NotFoundRegisterException : RootException
{
    public NotFoundRegisterException(string message) : base(message, 404, "Not found")
    {}
}