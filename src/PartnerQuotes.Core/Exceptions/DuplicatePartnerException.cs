using PartnerQuotes.Core.Exceptions;

namespace PartnerQuotes.Core.Exceptions;

public class DuplicatePartnerException(string message) : BaseException(message)
{
    public override int StatusCode => 409;
}
