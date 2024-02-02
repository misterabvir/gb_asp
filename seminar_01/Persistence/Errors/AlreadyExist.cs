using Persistence.Base;

namespace Persistence.Errors;

public class AlreadyExist(string message, string description) :
    Error(message, description)
{
    public override ErrorType Type => ErrorType.Conflict;
}
