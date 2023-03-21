using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceStarr.GlobalErrorHandling;

namespace FaceStarr.Controllers
{
    [Route("/errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController:BaseController
    {
        public IActionResult Error(int code)
        {
            return new OkObjectResult(new ErrorResponse(code));
        }
    }
}