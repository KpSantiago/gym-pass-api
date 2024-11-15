namespace GymPass.Shared.Exceptions;

public class NotFoundRegisterException : ArgumentException
{
    public NotFoundRegisterException(string message) : base(message) {}
}