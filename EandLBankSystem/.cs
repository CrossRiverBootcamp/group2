namespace Exceptions;

public class FailedToSaveChangesException : Exception
{
    public FailedToSaveChangesException()
    {
    }

    public FailedToSaveChangesException(string message)
        : base(message)
    {
    }

    public FailedToSaveChangesException(string message, Exception inner)
        : base(message, inner)
    {
    }
}