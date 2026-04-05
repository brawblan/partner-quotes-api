namespace PartnerQuotes.Core.Exceptions;

public abstract class BaseException : Exception
{
    public abstract int StatusCode { get; }

    protected BaseException(string message) : base(message) { }
    protected BaseException(string message, Exception inner) : base(message, inner) { }
}