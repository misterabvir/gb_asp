using Microsoft.AspNetCore.Mvc;
using StoreApplication.Base;

namespace StorePresentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult ProblemResult(IEnumerable<Error> errors)
    {
        return errors.First().Type switch
        {
            ErrorType.BadRequest => BadRequest(errors),
            ErrorType.Unauthorized => Unauthorized(errors),
            ErrorType.NotFound => NotFound(errors),
            ErrorType.Conflict => Conflict(errors),
            _ => Problem(),
        };
    }
}
