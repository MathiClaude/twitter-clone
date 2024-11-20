using Microsoft.AspNetCore.Mvc;
using TwitterClone.Domain.Common;

namespace TwitterClone.API.Utils;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }

        return new ObjectResult(new { Error = result.Error })
        {
            StatusCode = result.StatusCode
        };
    }

    public static IActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return new OkResult();
        }

        return new ObjectResult(new { Error = result.Error })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
