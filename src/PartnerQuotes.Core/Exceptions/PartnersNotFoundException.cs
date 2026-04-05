using PartnerQuotes.Core.Exceptions;

namespace PartnerQuotes.Core.Exceptions;

public class PartnersNotFoundException(string? nameFilter = "no filter") : BaseException($"No partners were found with the filter: {nameFilter}")
{
    public override int StatusCode => 404;
}