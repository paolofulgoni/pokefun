using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace PokeFun.Controllers
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment()
        {
            if (HttpContext != null)
            {
                var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

                return Problem(
                    detail: context?.Error.StackTrace,
                    title: context?.Error.Message);
            }
            else
            {
                return Problem();
            }
        }

        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
