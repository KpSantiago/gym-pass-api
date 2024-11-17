namespace GymPass.Shared.Exceptions;

public class ConflictInfosExcpetion : RootException
{
    public ConflictInfosExcpetion(string message) : base(message, 409, "Conflict") {}
}