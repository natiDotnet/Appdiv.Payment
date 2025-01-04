namespace Appdiv.Payment.CBEBirr.Exceptions;

public class MissingParameterException : Exception
{
    public MissingParameterException(string parameterName)
        : base($"{parameterName} is required")
    {
    }
}