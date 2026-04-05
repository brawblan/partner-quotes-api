using PartnerQuotes.Core.Exceptions;

namespace PartnerQuotes.Core.Exceptions;

public class PartnerNotFoundException(Guid guid) : BaseException($"partner with Guid '{guid}' was not found.")
{
    public override int StatusCode => 404;
}