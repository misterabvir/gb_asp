using Persistence.Base;

namespace Persistence.Errors;

public class Problem(string message, string description) : 
    Error(message, description)
{
    public override ErrorType Type => ErrorType.InternalServerError;
}
