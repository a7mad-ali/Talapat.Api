using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talapat.Api.Errors;

namespace Talapat.Api.Controllers
{
    [Route("Errors/Code")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : ControllerBase
    {   
        [HttpGet]
        public ActionResult Error(int Code)
        {
            if (Code == 401)
                return Unauthorized(new ApiResponse(401, null));
            else if (Code == 404)
                return NotFound(new ApiResponse(404, null));
            else
                return StatusCode(Code);
           
        }

    }
}
