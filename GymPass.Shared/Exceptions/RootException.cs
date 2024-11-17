using GymPass.Shared.Exceptions.ModuleExtensions;

namespace GymPass.Shared.Exceptions;

public class RootException : ArgumentException
{
    public RootException(string message, int satusCode, string errorType) : base(message)
    {
        this.SetErrorStatusCode(satusCode);
        this.SetErrorType(errorType);
    }
}