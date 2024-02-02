using Domain.Base;
using Persistence.Base;

namespace Persistence.Errors;

public class NotFound(string message, string description) :
    Error(message, description)
{
    public override ErrorType Type => ErrorType.NotFound;

}