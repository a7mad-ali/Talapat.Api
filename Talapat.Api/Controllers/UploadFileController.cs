using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talapat.Infrastructure.Helpers.UploadHandler;

namespace Talapat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        [HttpPost("Upload")]
        public IActionResult UploadFile(IFormFile file) 
        {
            return Ok(new UploadHandler().Upload(file));
        }
    }
}
