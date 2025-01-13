namespace Appdiv.Payment.Shared.Exceptions;

public class MissingParameterException : Exception
{
    public MissingParameterException(string parameterName)
        : base($"{parameterName} is required")
    {
    }
}