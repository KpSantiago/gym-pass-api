namespace GymPass.Shared.Exceptions.ModuleExtensions;

public static class ExceptionExtensions
{
    private static int ErrorStatusCode { get; set; }
    private static string ErrorType { get; set; }

    public static void SetErrorStatusCode(this Exception exception, int statusCode)
    {
        ErrorStatusCode = statusCode;
    }
    
    public static int GetErrorStatusCode(this Exception exception)
    {
        return ErrorStatusCode;
    }
    
    public static void SetErrorType(this Exception exception, string errorType)
    {
        ErrorType = errorType;
    }
    
    public static string GetErrorType(this Exception exception)
    {
        return ErrorType;
    }
}